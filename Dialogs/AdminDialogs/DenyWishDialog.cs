using FileDataBase.Types;
using Flower;
using Flower.Handlers;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using System;
using System.Threading;

namespace Dialogs.AdminDialogs;

[Serializable]
public class DenyWishDialog : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        int m = await Sender.SendMessage(User?.KeyValue, MessageArchieve.GetResourceMessage(ResourceMessageType.IsInDevelopment));

        Thread.Sleep(1000);
        await TelegramBot.DeleteMessageForAdmin(m);
        ProcessHandler.StopProcess(User);
    }
}
