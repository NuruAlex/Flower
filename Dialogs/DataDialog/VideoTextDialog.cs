using Flower.SupportClasses;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.DataDialog;

[Serializable]
public class VideoTextDialog : IDataProcess
{
    public TelegramMessage ErrorMessage { get; set; } = new TextMessage(UserHandler.CurrentUser?.KeyValue) { Text = "Кажется произошла ошибка, ожидалось видео или текст" };

    public VideoTextDialog(long? chatId)
    {
        ErrorMessage = new TextMessage(chatId) { Text = "Кажется произошла ошибка, ожидалось видео или текст" };
    }

    public bool IsCorrectData(Message message)
    {
        if (AvailableMessageTypes.VideoOrTextType().Contains(message.Type))
            return true;

        return false;
    }
}
