using Messages.Builders;
using Messages.Types;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Messages.Main;

public static class Builder
{
    private static MessageBuilder _builder;


    public static event Action<string> OnBuildError;

    private static MessageBuilder GetBuilder(MessageType type) => type switch
    {
        MessageType.Text => new TextBuilder(),
        MessageType.Video => new VideoBuilder(),
        MessageType.Voice => new VoiceBuilder(),
        MessageType.VideoNote => new VideoNoteBuilder(),
        MessageType.Photo => new PhotoBuilder(),
        MessageType.Audio => new AudioBuilder(),
        _ => new UnknownBuilder(),
    };

    /// <summary>
    /// Создает объект TelegramMessage
    /// </summary>
    /// <param name="message">Сообщение из которого необходимо создать </param>
    /// <returns>Новый объект из иерархии TelegramMessage
    /// или UnknownTelegramMessage</returns>
    public static async Task<TelegramMessage> CreateTelegramMessage(Message message)
    {
        _builder = GetBuilder(message.Type);
        try
        {
            return await _builder.CreateTelegramMessage(message);
        }
        catch (Exception ex)
        {
            OnBuildError.Invoke(ex.Message + $"TelegramMessageBuilder / CreateTelegramMessage / Inner builder type: {_builder?.GetType()}");
            return new UnknownTelegramMessage();
        }
    }

    /// <summary>
    /// Создает объект TelegramMessage
    /// </summary>
    /// <param name="chatId">Адрес получателя</param>
    /// <param name="message">Сообщение из которого необходимо создать</param>
    /// <returns></returns>
    public static async Task<TelegramMessage> CreateTelegramMessage(long chatId, Message message)
    {
        TelegramMessage result = await CreateTelegramMessage(message);

        result.ChatId = chatId;

        return result;
    }
}
