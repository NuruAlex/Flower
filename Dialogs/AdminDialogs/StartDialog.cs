using FileDataBase.Types;
using Flower;
using Flower.SupportClasses;
using Messages.Main;
using System;

namespace Dialogs.AdminDialogs;

[Serializable]
public class StartDialog : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        await Sender.SendMessage(User?.KeyValue, "Подождите секунду, создаем список команд...");
        await TelegramBot.LoadAdminCommandsAsync();
        await Sender.SendMessage(User?.KeyValue, "Если кнопка меню не появилась, обновите страницу");
    }
}
