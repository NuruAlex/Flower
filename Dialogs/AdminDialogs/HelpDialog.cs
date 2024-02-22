using FileDataBase.Types;
using Flower.Bot.Resources;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Types;
using System;

namespace Dialogs.AdminDialogs;

[Serializable]
public class HelpDialog : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public string GetCommandsList(CommandGroup group)
    {
        string message = "";
        group.Commands.ForEach(i => { message += $"{i.Command} - {i.Description}\n"; });
        return message;
    }

    public async void Start()
    {
        TextMessage message = new(User?.KeyValue);

        foreach (CommandGroup group in CommandArchieve.GetAdminCommands())
        {
            message.Text += $"\n{group.Title}\n\n";
            message.Text += GetCommandsList(group);
        }

        await Sender.SendMessage(message);
    }
}
