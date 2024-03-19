using Messages.Main;
using Messages.Types;
using Messages.Types.Media;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Messages.Senders;

[Serializable]
public class PhotoSender : MediaSender<TelegramPhoto>
{
    /// <summary>
    /// Send Messages with media 
    /// </summary>
    /// <param name="message"></param>
    /// <returns> returns -1 if some error</returns>
    public override async Task<int> SendMedia(TelegramMessage message, InputFile file)
        => (await SenderClient.Bot.SendPhotoAsync(chatId: message.ChatId, photo: file, caption: (message as PhotoMessage).Media.Caption, replyMarkup: message.Markup.ToMarkUp())).MessageId;

}
