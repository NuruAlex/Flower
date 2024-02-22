using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Main;
using System;
using Telegram.Bot.Types;

namespace Dialogs.AdminDialogs;

[Serializable]
public class MailingForAll : IOneActProcess
{
    private readonly AnswerDialog _dialog;

    public TelegramUser User { get; set; }

    public MailingForAll()
    {
        User = UserHandler.CurrentUser;

        _dialog = new(User?.KeyValue);
    }

    public async void NextAction(Message message)
    {
        if (!_dialog.IsCorrectData(message))
        {
            await Sender.SendMessage(_dialog.ErrorMessage);
            return;
        }

        await Sender.SendMailing(await Builder.CreateTelegramMessage(message), DataBase.Clients.GetAllKeys());

        await Sender.SendMessage(User.KeyValue, "Рассылка отправлена");
    }

    public async void Start() => await Sender.SendMessage(User?.KeyValue, "Это рассылка для всех, запишите свой ответ:");
}
