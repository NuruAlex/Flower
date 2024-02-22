using FileDataBase.Collections;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower;
using Flower.Handlers;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using System;
using System.Threading;

namespace Dialogs.AdminDialogs;

[Serializable]
public class DeleteItemProcess<T, k> : IStartProcess where T : ClicableObject<k>
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        var collection = DataBaseCollections.GetCollection<T>();

        if (collection == null)
        {
            int m1 = await Sender.SendMessage(User?.KeyValue, "Объект не может быть удален, коллекция не содержит ключ");

            Thread.Sleep(1000);
            await TelegramBot.DeleteMessageForAdmin(m1);
            return;
        }

        (collection as ClicableObjectCollection<T, k>).DeleteDefault(ItemArchieve.GetItem<T>());


        int m = await Sender.SendMessage(User?.KeyValue, "Объект удален");
        Thread.Sleep(1000);
        await TelegramBot.DeleteMessageForAdmin(m);

        ProcessHandler.Run(User, new PrintListProcess<T, k>());
    }
}
