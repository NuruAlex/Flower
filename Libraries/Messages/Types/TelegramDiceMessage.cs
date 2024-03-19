using Telegram.Bot.Types.Enums;

namespace Messages.Types;

public class TelegramDiceMessage : TelegramMessage
{

    public TelegramDiceMessage() : base() { }
    public TelegramDiceMessage(long? chatId) : base(chatId) { }

    public override MessageType Type => MessageType.Dice;

    public string BeforeText { get; set; }


}
