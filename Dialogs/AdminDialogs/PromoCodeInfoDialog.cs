using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Bot;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using System;
using System.Collections.Generic;

namespace Dialogs.AdminDialogs;

[Serializable]
public class PromoCodeInfoDialog : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        DiscontPromoCode promoCode = CallBackData.PromoCode as DiscontPromoCode;

        List<Payment> payments = DataBase.Payments.FindAsList(i => i is DiscontPayment payment && payment.Promocode?.KeyValue == promoCode.KeyValue && i.Aproved);

        double resultPrice = 0;
        payments.ForEach(i => { resultPrice += i.Price; });

        await Sender.SendMessage(User?.KeyValue,
            $"{promoCode.KeyValue} - {resultPrice} рублей",
            new InlineMarkup(new InlineButton("Назад к списку промокодов", new CallBackPacket(CallBackCode.PromoCodeList))));
    }
}
