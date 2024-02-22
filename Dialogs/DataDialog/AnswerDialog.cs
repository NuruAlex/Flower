using Flower.SupportClasses;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.DataDialog;

[Serializable]
public class AnswerDialog : IDataProcess
{
    public AnswerDialog(long? chatId)
    {
        ErrorMessage.ChatId = chatId ?? 0;
    }

    public TelegramMessage ErrorMessage { get; set; } = new TextMessage() { Text = "Кажется произошла ошибка, ожидается видео, текст или аудио" };


    public bool IsCorrectData(Message message)
    {
        if (AvailableMessageTypes.AnswerTypes().Contains(message.Type))
            return true;

        return false;
    }
}
