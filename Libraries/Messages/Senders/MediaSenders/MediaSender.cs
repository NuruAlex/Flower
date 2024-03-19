using Messages.Types;
using Messages.Types.Media;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Senders;

[Serializable]
public abstract class MediaSender<T> : MessageSender where T : TelegramMedia
{
    /// <summary>
    /// Send Messages with media 
    /// </summary>
    /// <param name="message"></param>
    /// <returns>-1 if some error else message ID</returns>
    public override async Task<int> SendMessage(TelegramMessage message)
    {

        if ((message as MediaMessage<T>).Media is null) return -1;

        T item = (message as MediaMessage<T>).Media;

        if (item.Path != null && System.IO.File.Exists(item.Path))
        {
            using Stream stream = System.IO.File.OpenRead(item.Path);
            return await SendMedia(message, InputFile.FromStream(stream));
        }
        else if (item.UriOrFileId != null)
            return await SendMedia(message, InputFile.FromString(item.UriOrFileId));
        return -1;
    }
    /// <summary>
    /// Send Messages with media 
    /// </summary>
    /// <param name="message"></param>
    /// <returns> returns -1 if some error</returns>
    public abstract Task<int> SendMedia(TelegramMessage message, InputFile file);
}
