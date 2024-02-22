using FileDataBase.Main;
using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.DataDialog;

[Serializable]
public class PromoProcess : IDataProcess
{
    public bool IsNewPromo { get; set; }
    public TelegramMessage ErrorMessage { get; set; } = new TextMessage(UserHandler.CurrentUser?.KeyValue) { Text = "Такой промокод уже существует" };
    private PromoCode PromoCode { get; set; }
    public bool IsCorrectData(Message message)
    {
        if (message.Text == null)
            return false;

        PromoCode = DataBase.PromoCodes.Find(i => i.KeyValue == message.Text.ToLower());

        if (IsNewPromo)
            return PromoCode == null;
        else
            return PromoCode != null;
    }
}
