using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Types;
using System;

namespace Dialogs.AdminDialogs;

[Serializable]
public class DenyPaymentRequestDialog : IStartProcess
{
    public TelegramUser User { get => UserHandler.CallBackUser; set { } }

    public async void Start()
    {
        if (PaymentsHandler.IsClientPayed(User as Client))
        {
            await Sender.SendMessage(new TextMessage(User?.KeyValue) { Text = "Администратор не одобрил ваш перевод" });
            await Sender.SendMessage(new TextMessage(UserHandler.CurrentUser?.KeyValue) { Text = $"{User.KeyValue} - запрещено" });
            User.Update();
        }
    }
}
