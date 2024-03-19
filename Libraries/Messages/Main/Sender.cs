using Messages.Reply.Markups;
using Messages.Senders;
using Messages.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace Messages.Main;

public static class Sender
{
    private static MessageSender _sender;

    private static MessageSender GetSender(MessageType type) => type switch
    {
        MessageType.Text => new TextSender(),
        MessageType.Video => new VideoSender(),
        MessageType.Voice => new VoiceSender(),
        MessageType.VideoNote => new VideoNoteSender(),
        MessageType.Photo => new PhotoSender(),
        MessageType.Contact => new ContactSender(),
        MessageType.Audio => new AudioSender(),
        MessageType.Dice => new DiceSender(),
        _ => new UnknownSender(),
    };


    public static event Action<string> OnSendError;

    /// <summary>
    /// Метод отправляет объект TelegramMessage
    /// </summary>
    /// <param name="message">Может быть любым из иерархии объектов <see cref="TelegramMessage"/></param>
    /// <returns>
    ///     Возврат: Id  отправленного собщения.
    /// В случае отправки Dice возвращает выпавший результат.
    /// Если сообщение не отправилось возвращает -1
    /// </returns>
    public static async Task<int> SendMessage(TelegramMessage message)
    {
        try
        {
            _sender = GetSender(message.Type);
            return await _sender.SendMessage(message);
        }
        catch (Exception ex)
        {
            OnSendError.Invoke(ex.Message + $" Sender / SendMessage() / Inner Sender Type {_sender.GetType().Name}");
        }
        return -1;
    }

    /// <summary>
    /// Метод отправляет текстовое сообщение
    /// </summary>
    /// <param name="chatId">Адрес получателя</param>
    /// <param name="text">Текст для отправки</param>
    /// <param name="markup">Кнопки или их отсутсвие</param>
    /// <returns>Возврат: Id  отправленного собщения. Если сообщение не отправилось: -1</returns>
    public static async Task<int> SendMessage(long? chatId, string text, IMarkup markup = null) => await SendMessage(new TextMessage(chatId)
    {
        Text = text,
        Markup = markup
    });

    /// <summary>
    /// Метод отправляет Объект <see cref="TelegramMessage"/>, не имеющего адреса получателя
    /// </summary>
    /// <param name="chatId">Адрес получателя</param>
    /// <param name="message">Объект <see cref="TelegramMessage"/> без адреса получателя</param>
    /// <returns>Возврат: Id  отправленного собщения. Если сообщение не отправилось: -1</returns>
    public static async Task<int> SendMessage(long? chatId, TelegramMessage message)
    {
        message.ChatId = chatId ?? 0;
        return await SendMessage(message);
    }

    /// <summary>
    /// Метод отправляет объект <see cref="MultiMessage"/>
    /// </summary>
    /// <param name="multiMessage">Сообщение, которое содержит в себе несколько подсообщений любого типа из иерархии <see cref="TelegramMessage"/></param>
    /// <returns></returns>
    public static async Task SendMultiMessage(MultiMessage multiMessage)
    {
        if (multiMessage == null) return;

        foreach (TelegramMessage message in multiMessage.Messages)
        {
            message.ChatId = multiMessage.ChatId;
            await SendMessage(message);
        }
    }

    /// <summary>
    /// Мтод отправляет объект <see cref="TelegramMessage"/> нескольким получателям
    /// </summary>
    /// <param name="message">Сообщение для отправки</param>
    /// <param name="mailingList">Список адресов получателей</param>
    /// <returns></returns>
    public static async Task SendMailing(TelegramMessage message, List<long> mailingList)
    {
        if (message == null) return;

        foreach (long adress in mailingList)
        {
            message.ChatId = adress;
            await SendMessage(message);
        }
    }

}
