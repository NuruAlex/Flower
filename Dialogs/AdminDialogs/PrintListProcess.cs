using FileDataBase.Types;
using Flower.SupportClasses;
using MessageConverters;
using Messages.Main;
using System;

namespace Dialogs.AdminDialogs;

[Serializable]
public class PrintListProcess<T, k> : IStartProcess where T : ClicableObject<k>
{
    public TelegramUser User { get; set; }

    public async void Start() => await Sender.SendMessage(Converter.ConvertCollection<T, k>(UserHandler.CurrentUser));
}
