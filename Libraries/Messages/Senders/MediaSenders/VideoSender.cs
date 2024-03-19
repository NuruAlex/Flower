using Messages.Main;
using Messages.Types;
using Messages.Types.Media;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Messages.Senders;

[Serializable]
public class VideoSender : MediaSender<TelegramVideo>
{
    public override async Task<int> SendMedia(TelegramMessage message, InputFile file)
        => (await SenderClient.Bot.SendVideoAsync(message.ChatId, file, replyMarkup: message.Markup.ToMarkUp())).MessageId;

}
