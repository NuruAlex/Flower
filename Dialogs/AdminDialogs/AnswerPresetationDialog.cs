using Dialogs.DataDialog;
using FileDataBase.Types;
using Flower;
using Flower.Bot;
using Flower.Dialogs.Sub;
using Flower.Handlers;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot.Types;

namespace Dialogs.AdminDialogs;

[Serializable]
public class AnswerPresetationDialog : IOneActProcess
{
    private readonly Presentation _presentation;
    private readonly List<int> _messages;
    private readonly ISubProcess _subProcess;
    public TelegramUser User { get; set; }

    public AnswerPresetationDialog()
    {
        _presentation = CallBackData.Presentation;

        _messages = new();

        _subProcess = new BuildAndSendSubProcess();

        User = UserHandler.CallBackUser;
    }

    public void Clear()
    {
        Thread.Sleep(1000);
        TelegramBot.DeleteAllMessages(_messages);
        ProcessHandler.StopProcess(User);
    }

    public async void NextAction(Message parameter)
    {
        if (!new AnswerDialog(User?.KeyValue).IsCorrectData(parameter))
        {
            await Sender.SendMessage(new AnswerDialog(User?.KeyValue).ErrorMessage);
            Clear();
            return;
        }

        _messages.Add(await _subProcess.Start(User, parameter, "Ответ на самопрезентацию отправлен"));

        _presentation.HasAnswered = true;
    }

    public async void Start()
    {
        if ((bool)(_presentation?.HasAnswered))
        {
            _messages.Add(await Sender.SendMessage(UserHandler.CurrentUser?.KeyValue, MessageArchieve.GetResourceMessage(ResourceMessageType.PresentationAlreadyAnswered)));
            Clear();
            return;
        }

        _messages.Add(await Sender.SendMessage(UserHandler.CurrentUser?.KeyValue, MessageArchieve.GetResourceMessage(ResourceMessageType.WritePresentationAnswer)));
    }
}
