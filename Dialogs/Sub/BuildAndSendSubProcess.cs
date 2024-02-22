using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Types;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Flower.Dialogs.Sub;



[Serializable]
public class BuildAndSendSubProcess : ISubProcess
{
    public async Task<int> Start(TelegramUser user, Message message, string suscessMessage)
    {
        TelegramMessage data = await Builder.CreateTelegramMessage(user.KeyValue, message);
        await Sender.SendMessage(data);
        return await Sender.SendMessage(UserHandler.CurrentUser.KeyValue, suscessMessage);
    }
}
