using Messages.Types;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Builders;

[Serializable]
public abstract class MessageBuilder
{
    /// <summary>
    /// Build  new TelegramMessage from Message
    /// </summary>
    /// <param name="message">source to build</param>
    /// <returns> TelegramMessage item</returns>
    public abstract Task<TelegramMessage> CreateTelegramMessage(Message message);
}

