
using Messages.Types;
using Messages.Types.Media;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Builders;

[Serializable]
public class PhotoBuilder : MediaBuilder<TelegramPhoto>
{
    public override TelegramMessage BuildWithUriOrFileId(string uriOrFileId) => new PhotoMessage()
    {
        Media = new TelegramPhoto()
        {
            UriOrFileId = uriOrFileId
        },
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message) => new PhotoMessage()
    {
        Media = new TelegramPhoto()
        {
            Path = await new Downloader().DowloadIfNotExist(message.Photo[message.Photo.Length - 1]),
        }
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message, string downloadPath) => new PhotoMessage()
    {
        Media = new TelegramPhoto()
        {
            Path = await new Downloader().DowloadIfNotExist(message.Photo[message.Photo.Length - 1], downloadPath),
        }
    };
}
