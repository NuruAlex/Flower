using Dialogs.DataDialog;
using FileDataBase.Types;
using Flower;
using Flower.Dialogs.Sub;
using Flower.Handlers;
using Flower.SupportClasses;
using Messages.Main;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot.Types;

namespace Dialogs.AdminDialogs;

[Serializable]
public class WriteToClientDialog : IOneActProcess
{
    private readonly List<int> _messages = new();
    private readonly AnswerDialog _dialog;
    private readonly BuildAndSendSubProcess _subProcess;
    public TelegramUser User { get; set; }

    public WriteToClientDialog()
    {
        User = UserHandler.CurrentUser;
        _dialog = new AnswerDialog(User?.KeyValue);
        _subProcess = new();
    }

    public async void NextAction(Message message)
    {
        _messages.Add(message.MessageId);

        if (!_dialog.IsCorrectData(message))
        {
            _messages.Add(await Sender.SendMessage(_dialog.ErrorMessage));
            return;
        }

        _messages.Add(await _subProcess.Start(User, message, "Ответ отправлен"));

        Thread.Sleep(1000);
        TelegramBot.DeleteAllMessages(_messages);
        ProcessHandler.Run(User, new PrintItemProcess<Client, long>());
    }

    public async void Start() => _messages.Add(await Sender.SendMessage(User?.KeyValue, "Запишите свое сообщение"));
}
