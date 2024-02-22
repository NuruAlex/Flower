using FileDataBase.Main;
using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Main;
using System;


namespace Dialogs.AdminDialogs;

[Serializable]
public class NewAdminDialog : IStartProcess
{

    public TelegramUser User { get => UserHandler.CallBackUser; set { } }

    public async void Start()
    {

        if (User == null || User as Client == null)
        {
            await Sender.SendMessage(UserHandler.CurrentUser?.KeyValue, "Клиент не найден");
            return;
        }

        if (DataBase.Admins.ContainsKey(User.KeyValue))
        {
            await Sender.SendMessage(UserHandler.CurrentUser?.KeyValue, "Администратор с таким id уже существует");
            return;
        }

        DataBase.Admins.Add(new Admin(User.KeyValue, User.UserName));

        DataBase.Clients.DeleteDefault(User as Client);
        await Sender.SendMessage(UserHandler.CurrentUser?.KeyValue, "Администратор добавлен");
    }
}
