using FileDataBase.Types;
using Flower.Resources;
using Flower.SupportClasses;
using MessageConverters;
using Messages.Main;
using Messages.Types;
using System;

namespace Dialogs.AdminDialogs;

[Serializable]
public class PrintItemProcess<T, k> : IStartProcess where T : ClicableObject<k>
{
    public TelegramUser User { get; set; }

    public async void Start()
    {
        T _item = ItemArchieve.GetItem<T>();

        TelegramMessage data = Converter.ConvertItem(UserHandler.CurrentUser, _item);

        await Sender.SendMessage(data);
    }
}
