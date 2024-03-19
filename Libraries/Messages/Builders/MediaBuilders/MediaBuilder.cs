using Messages.Types;
using Messages.Types.Media;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Builders;

[Serializable]
public abstract class MediaBuilder<T> : MessageBuilder where T : TelegramMedia
{
    public abstract TelegramMessage BuildWithUriOrFileId(string uriOrFileId);
    public abstract Task<TelegramMessage> CreateTelegramMessage(Message message, string downloadPath);
}
