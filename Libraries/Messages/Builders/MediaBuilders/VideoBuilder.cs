
using Messages.Types;
using Messages.Types.Media;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Builders;

[Serializable]
public class VideoBuilder : MediaBuilder<TelegramVideo>
{
    public override TelegramMessage BuildWithUriOrFileId(string uriOrFileId) => new VideoMessage()
    {
        Media = new TelegramVideo()
        {
            UriOrFileId = uriOrFileId
        },
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message) => new VideoMessage()
    {
        Media = new TelegramVideo()
        {
            Path = await new Downloader().DowloadIfNotExist(message.Video),
        }
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message, string downloadPath) => new VideoMessage()
    {
        Media = new TelegramVideo()
        {
            Path = await new Downloader().DowloadIfNotExist(message.Video, downloadPath),
        }
    };
}
