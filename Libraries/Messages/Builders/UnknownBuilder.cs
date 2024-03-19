using Messages.Types;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Builders;

[Serializable]
public class UnknownBuilder : MessageBuilder
{
    public override Task<TelegramMessage> CreateTelegramMessage(Message message)
    {
        throw new NotImplementedException();
    }
}
