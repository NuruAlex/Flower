using System.Collections.Generic;

namespace Messages.Types;

[System.Serializable]
public class MultiMessage
{
    public long ChatId { get; set; }

    public List<TelegramMessage> Messages;

    public MultiMessage(long? chatId, List<TelegramMessage> messages)
    {
        ChatId = chatId ?? 0;
        Messages = messages ?? new List<TelegramMessage>();
    }
}
