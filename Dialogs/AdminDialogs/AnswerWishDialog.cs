using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower;
using Flower.Bot;
using Flower.Dialogs.Sub;
using Flower.Handlers;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot.Types;

namespace Dialogs.AdminDialogs;

[Serializable]
public class AnswerWishDialog : IOneActProcess
{
    private readonly IDataProcess _dialog;
    private readonly List<int> _messages;
    private readonly Wish _wish;
    private readonly ISubProcess _subDialog;

    public TelegramUser User { get; set; }

    public AnswerWishDialog()
    {
        User = UserHandler.CallBackUser;

        _messages = new();

        _wish = DataBase.Wishes.FindKey(CallBackData.Packet.Number);

        _dialog = new AnswerDialog(UserHandler.CurrentUser?.KeyValue);

        _subDialog = new BuildAndSendSubProcess();
    }

    public async void NextAction(Message message)
    {
        _messages.Add(message.MessageId);

        if (!_dialog.IsCorrectData(message))
        {
            _messages.Add(await Sender.SendMessage(_dialog.ErrorMessage));
            EndProcess();
            return;
        }

        _messages.Add(await _subDialog.Start(User, message, "Ответ на желание отправлен"));

        _wish.HasAnswered = true;
        EndProcess();
    }

    public void EndProcess()
    {
        Thread.Sleep(1000);
        TelegramBot.DeleteAllMessages(_messages);
        ProcessHandler.StopProcess(UserHandler.CurrentUser);
    }

    public async void Start()
    {
        if (_wish?.HasAnswered == true)
        {
            _messages.Add(await Sender.SendMessage(new TextMessage(UserHandler.CurrentUser?.KeyValue)
            {
                Text = "Желание уже имеет ответ"
            }));
            EndProcess();
        }

        _messages.Add(await Sender.SendMessage(new TextMessage(UserHandler.CurrentUser?.KeyValue)
        {
            Text = "Запишите свой ответ на желание:"
        }));
    }
}




