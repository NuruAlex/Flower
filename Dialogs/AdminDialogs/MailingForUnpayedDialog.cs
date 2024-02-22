using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Handlers;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.AdminDialogs;

[Serializable]
public class MailingForUnpayedDialog : IOneActProcess
{

    private readonly IDataProcess _dialog;
    public TelegramUser User { get; set; }

    public MailingForUnpayedDialog()
    {
        User = UserHandler.CurrentUser;

        _dialog = new AnswerDialog(User?.KeyValue);
    }

    public async void NextAction(Message parameter)
    {
        if (!_dialog.IsCorrectData(parameter))
        {
            await Sender.SendMessage(_dialog.ErrorMessage);
            return;
        }

        TelegramMessage data = await Builder.CreateTelegramMessage(parameter);

        await Sender.SendMailing(data, DataBase.Clients.GetKeys(i => PaymentsHandler.GetClientPaymentsCount(i) == 0));

        await Sender.SendMessage(User.KeyValue, "Рассылка отправлена");

        ProcessHandler.StopProcess(User);
    }

    public async void Start() => await Sender.SendMessage(User?.KeyValue, "Это рассылка для тех, кто не оплатил, запишите свой ответ");
}
