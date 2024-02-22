using Dialogs.ClientDialogs;
using FileDataBase.Types;
using Flower;
using Flower.Bot;
using Flower.Handlers;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Types;
using System;
using System.Threading;

namespace Dialogs.AdminDialogs;

[Serializable]
public class AprovePaymentDialog : IStartProcess
{
    public TelegramUser User { get; set; }

    public AprovePaymentDialog()
    {
        User = UserHandler.CallBackUser;
    }

    public async void Start()
    {
        CallBackData.Payment.Aproved = true;

        TextMessage message = new(User?.KeyValue)
        {
            Text = "Администратор разрешил вам двигаться дальше:), поэтому смело бросайте кубик"
        };

        if (CallBackData.Payment is BotPayment)
            (User as Client).RemainingMoves = -1;
        else
        {
            (User as Client).RemainingMoves = 0;
            message.Text = "Администратор одобрил ваш перевод, подождите, скоро вам прийдет сообщение о времени и месте проведения игры";
        }

        await Sender.SendMessage(message);

        CallBackData.Payment.Aproved = true;
        CallBackData.Payment.Update();

        int m = await Sender.SendMessage(new TextMessage(UserHandler.CurrentUser?.KeyValue)
        {
            Text = $"{(User as Client).KeyValue} - разрешено"
        });

        Thread.Sleep(1000);
        await TelegramBot.DeleteMessageForAdmin(m);
        ProcessHandler.Run(User, new MoveProcess(User as Client));
    }
}
