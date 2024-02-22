using Messages.Main;
using Messages.Reply.Markups;
using Messages.Types;
using System.Threading.Tasks;

namespace Flower.SupportClasses.Senders;

public static class DiceSender
{
    public static async Task<int> SendDiceAsync(long chatId) => await Sender.SendMessage(new TelegramDiceMessage()
    {
        ChatId = chatId,
        BeforeText = "Кубик сейчас начнет крутиться:)",
        Markup = new RemoveMarkup()
    });
}
