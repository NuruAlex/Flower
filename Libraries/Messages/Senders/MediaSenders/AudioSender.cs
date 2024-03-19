using Messages.Main;
using Messages.Types;
using Messages.Types.Media;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Messages.Senders;

public class AudioSender : MediaSender<TelegramAudio>
{
    public override async Task<int> SendMedia(TelegramMessage message, InputFile file)
        => (await SenderClient.Bot.SendAudioAsync(chatId: message.ChatId, audio: file, caption: (message as AudioMessage).Media.Title, replyMarkup: message.Markup.ToMarkUp())).MessageId;
}