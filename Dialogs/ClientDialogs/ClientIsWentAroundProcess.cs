using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Main;
using System;

namespace Dialogs.ClientDialogs;

[Serializable]
public class ClientIsWentAroundProcess : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        if (MoveHandler.IsClientWentAround(User as Client))
            await Sender.SendMessage(User.KeyValue, "Вы прошли полный круг");
    }
}
