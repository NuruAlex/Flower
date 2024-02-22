using Dialogs;
using Dialogs.ClientDialogs;
using Flower.Bot;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Reply.CallBack;
using Messages.Types;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Flower.Handlers;

public static class CallBackHandler
{
    private static Message _message;
    private static CallBackPacket _packet;


    private static readonly TelegramMessage _callBackError = new TextMessage()
    {
        Text = "Команда не распознана"
    };

    public static void StartHandle() => TelegramBot.OnCallBack += TelegramBot_OnCallBack;

    private static async void TelegramBot_OnCallBack(CallBackPacket packet, Message message)
    {
        _packet = packet;
        _message = message;
        _callBackError.ChatId = UserHandler.CurrentUser.KeyValue;
        CallBackData.LoadData(_packet);

        if (UserHandler.IsAdmin())
            await AnalyzeAdminCallBack();
        if (UserHandler.IsClient())
            AnalyzeClientCallBack();
    }

    public static async void StartCallBackProcess()
    {
        IStartProcess process = CallBackCommandsArchieve.GetProcess(_packet.Code);

        if (process.GetType() == typeof(UnknownProcess))
        {
            await Sender.SendMessage(_callBackError);
            return;
        }

        ProcessHandler.Run(UserHandler.CurrentUser, process);
    }

    public static void AnalyzeClientCallBack()
    {
        if (_packet.Code == CallBackCode.Ingnore)
            return;

        IStartProcess startProcess = ProcessHandler.GetProcess(UserHandler.CurrentUser.KeyValue);

        if (startProcess is PromocodeOrPayDialog)
        {
            ProcessHandler.NextAction(UserHandler.CurrentUser.KeyValue, _message, _packet);
            return;
        }

        StartCallBackProcess();
    }

    public static async Task AnalyzeAdminCallBack()
    {
        if (_packet.Code != CallBackCode.Ingnore && _message != null)
            await TelegramBot.DeleteMessageForAdmin(_message.MessageId);

        StartCallBackProcess();
    }

}
