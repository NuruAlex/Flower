using Dialogs;
using Dialogs.ClientDialogs;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Types;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Flower.Handlers;

public static class MessageHandler
{
    private static Message _message;
    private static readonly TelegramMessage _messageError = new TextMessage()
    {
        Text = "Кажется произошла ошибка, сообщение не распознано"
    };

    public static void StartHandle() => TelegramBot.OnMessage += TelegramBot_OnMessage;

    private static async void TelegramBot_OnMessage(Message message)
    {
        _message = message;

        if (UserHandler.IsAdmin())
            await UseAsAdmin();
        if (UserHandler.IsClient())
            await UseAsClient();
    }
    public static async void StartProcess()
    {
        if (ProcessHandler.NextAction(UserHandler.CurrentUser.KeyValue, _message)) return;

        if (_message.Text == null) return;

        IStartProcess process = new UnknownProcess();

        if (UserHandler.IsAdmin())
            process = CommandArchieve.GetAdminProcess(_message.Text.ToLower());
        if (UserHandler.IsClient())
            process = CommandArchieve.GetClientProcess(_message.Text.ToLower());

        if (process.GetType() == typeof(UnknownProcess))
        {
            _messageError.ChatId = UserHandler.CurrentUser.KeyValue;
            await Sender.SendMessage(_messageError);
            return;
        }

        ProcessHandler.Run(UserHandler.CurrentUser, process);
    }

    public static async Task UseAsClient()
    {
        await TelegramBot.Bot.DeleteMyCommandsAsync();

        ProcessHandler.RunDefault();

        if (_message?.Text == "/help")
        {
            new ClientHelpProcess()
            {
                User = UserHandler.CurrentUser
            }.Start();

            return;
        }
        if (_message?.Text?.ToLower() == "отмена")
        {
            ProcessHandler.StopProcess(UserHandler.CurrentUser);
            await Sender.SendMessage(UserHandler.CurrentUser?.KeyValue, "Отменено");
            return;
        }

        StartProcess();
    }

    public static async Task UseAsAdmin()
    {
        if (_message?.Text?.ToLower() == "отмена")
        {
            ProcessHandler.StopProcess(UserHandler.CurrentUser);
            await Sender.SendMessage(UserHandler.CurrentUser?.KeyValue, "Отменено");
            return;
        }


        if (AdminCommandsHandler.IsCommand(_message?.Text))
        {
            if (AdminCommandsHandler.IsAdminUsedCommand(_message?.Text))
            {
                await TelegramBot.DeleteMessageForAdmin(_message.MessageId);
                return;
            }
            AdminCommandsHandler.SetUsedCommand(_message.Text, _message.MessageId);
        }

        StartProcess();
    }
}
