
using Messages.Types;
using Messages.Types.Media;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Builders;

[Serializable]
public class VideoNoteBuilder : MediaBuilder<TelegramVideoNote>
{
    public override TelegramMessage BuildWithUriOrFileId(string uriOrFileId) => new VideoNoteMessage()
    {
        Media = new TelegramVideoNote()
        {
            UriOrFileId = uriOrFileId
        },
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message) => new VideoNoteMessage()
    {
        Media = new TelegramVideoNote()
        {
            Path = await new Downloader().DowloadIfNotExist(message.VideoNote),
        }
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message, string downloadPath) => new VideoNoteMessage()
    {
        Media = new TelegramVideoNote()
        {
            Path = await new Downloader().DowloadIfNotExist(message.VideoNote, downloadPath),
        }
    };
}
