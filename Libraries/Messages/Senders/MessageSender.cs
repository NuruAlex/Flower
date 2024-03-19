using Messages.Types;
using System;
using System.Threading.Tasks;

namespace Messages.Senders;

[Serializable]
public abstract class MessageSender
{
    /// <summary>
    /// Methods Sends Message
    /// </summary>
    /// <param name="message">The message that need to send</param>
    /// <returns>if something goes wrong returns -1 else returns message id</returns>
    public abstract Task<int> SendMessage(TelegramMessage message);

}








