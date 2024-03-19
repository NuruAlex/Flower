using Messages.Types;
using System;
using System.Threading.Tasks;

namespace Messages.Senders;

[Serializable]
public class UnknownSender : MessageSender
{
    public override Task<int> SendMessage(TelegramMessage message)
    {
        throw new NotImplementedException();
    }
}
