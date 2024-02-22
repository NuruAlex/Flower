using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Main;
using System;

namespace Dialogs.AdminDialogs;

[Serializable]
public class StaticsticsDialog : IStartProcess
{
    public TelegramUser User { get => UserHandler.CallBackUser; set { } }

    public async void Start() => await Sender.SendMessage(ClientInfoManager.Statistics(User as Client));
}

[Serializable]
public class SummaryDialog : IStartProcess
{
    public TelegramUser User { get => UserHandler.CallBackUser; set { } }
    public async void Start() => await Sender.SendMessage(ClientInfoManager.Summary(User as Client));
}

[Serializable]
public class MoreDetailsDialog : IStartProcess
{
    public TelegramUser User { get => UserHandler.CallBackUser; set { } }
    public async void Start() => await Sender.SendMessage(ClientInfoManager.Detailes(User as Client));
}
