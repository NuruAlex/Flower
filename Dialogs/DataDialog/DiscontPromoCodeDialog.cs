using FileDataBase.Main;
using FileDataBase.Types;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.DataDialog;

[Serializable]
public class DiscontPromoCodeDialog : IDataProcess
{
    private readonly Client _client;
    public DiscontPromoCode PromoCode { get; set; }
    public TelegramMessage ErrorMessage { get; set; }

    public DiscontPromoCodeDialog(Client client) => _client = client;


    public bool IsCorrectData(Message message)
    {
        if (message.Text == null)
            return false;

        PromoCode = DataBase.PromoCodes.Find(i => i.KeyValue == message.Text) as DiscontPromoCode;

        if (PromoCode == null)
            return false;

        if (DataBase.Payments.CountOf(i => i.ChatId == _client.KeyValue && i is DiscontPayment payment && payment.Promocode?.KeyValue == PromoCode.KeyValue) != 0)
            return false;
        return true;
    }

}
