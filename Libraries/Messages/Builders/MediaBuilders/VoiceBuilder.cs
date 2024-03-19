
using Messages.Types;
using Messages.Types.Media;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Builders;

[Serializable]
public class VoiceBuilder : MediaBuilder<TelegramVoice>
{
    public override TelegramMessage BuildWithUriOrFileId(string uriOrFileId) => new VoiceMessage()
    {
        Media = new TelegramVoice()
        {
            UriOrFileId = uriOrFileId
        },
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message) => new VoiceMessage()
    {
        Media = new TelegramVoice()
        {
            Path = await new Downloader().DowloadIfNotExist(message.Voice),
        }
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message, string downloadPath) => new VoiceMessage()
    {
        Media = new TelegramVoice()
        {
            Path = await new Downloader().DowloadIfNotExist(message.Voice, downloadPath),
        }
    };
}
