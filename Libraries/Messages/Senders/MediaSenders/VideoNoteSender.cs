using Messages.Main;
using Messages.Types;
using Messages.Types.Media;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Messages.Senders;

public class VideoNoteSender : MediaSender<TelegramVideoNote>
{
    public override async Task<int> SendMedia(TelegramMessage message, InputFile file)
        => (await SenderClient.Bot.SendVideoNoteAsync(message.ChatId, file, replyMarkup: message.Markup.ToMarkUp())).MessageId;

}
