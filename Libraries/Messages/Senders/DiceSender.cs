using Messages.Main;
using Messages.Types;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Messages.Senders;

public class DiceSender : MessageSender
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns>Dice Result</returns>
    public override async Task<int> SendMessage(TelegramMessage message)
    {
        if (message is TelegramDiceMessage diceMessage)
        {
            await Sender.SendMessage(new TextMessage()
            {
                ChatId = diceMessage.ChatId,
                Text = diceMessage.BeforeText ?? "",
                Markup = diceMessage.Markup,
            });

            int result = (await SenderClient.Bot.SendDiceAsync(diceMessage.ChatId)).Dice.Value;

            Thread.Sleep(5500);

            return result;
        }
        return -1;
    }
}
