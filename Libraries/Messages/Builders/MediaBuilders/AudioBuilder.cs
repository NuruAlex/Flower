using Messages.Types;
using Messages.Types.Media;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Builders;

[Serializable]
public class AudioBuilder : MediaBuilder<TelegramAudio>
{
    public override TelegramMessage BuildWithUriOrFileId(string uriOrFileId) => new AudioMessage()
    {
        Media = new TelegramAudio()
        {
            UriOrFileId = uriOrFileId
        },
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message) => new AudioMessage()
    {
        Media = new TelegramAudio()
        {
            Path = await new Downloader().DowloadIfNotExist(message.Audio),
        }
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message, string downloadPath) => new AudioMessage()
    {
        Media = new TelegramAudio()
        {
            Path = await new Downloader().DowloadIfNotExist(message.Audio, downloadPath),
        }
    };
}
