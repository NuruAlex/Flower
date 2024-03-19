using Messages.Main;
using Messages.Types;
using Messages.Types.Media;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Messages.Senders;

[Serializable]
public class VoiceSender : MediaSender<TelegramVoice>
{
    public override async Task<int> SendMedia(TelegramMessage message, InputFile file)
        => (await SenderClient.Bot.SendVoiceAsync(message.ChatId, file, replyMarkup: message.Markup.ToMarkUp())).MessageId;
}
