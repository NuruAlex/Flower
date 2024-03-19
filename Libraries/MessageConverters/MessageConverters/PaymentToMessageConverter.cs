using FileDataBase.Main;
using FileDataBase.Types;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Types;
using System;

namespace MessageConverters;

[Serializable]
public class PaymentToMessageConverter : TelegramMessageConverter<Payment>
{
    public override string AsListItem(Payment item) => item.UserName ?? item.ChatId.ToString();

    public override TelegramMessage ConvertCollection(TelegramUser user) => MessageCreator.CreateMessageList<Payment, int>(user, DataBase.Payments.Items);

    public override TelegramMessage ConvertItem(TelegramUser user, Payment item)
    {
        if (item.Aproved) return new PhotoMessage(user?.KeyValue)
        {
            Media = new()
            {
                Caption = item.GetCaptionString(),
                Path = item.PhotoPath,
            },
            Markup = new InlineMarkup(new InlineButton("Назад", new CallBackPacket(CallBackCode.PaymentsList)))

        };
        else return new PhotoMessage(user?.KeyValue)
        {
            Media = new()
            {
                Caption = item.GetCaptionString(),
                Path = item.PhotoPath,
            },
            Markup = new InlineMarkup(
                new InlineButton("Разрешить", new CallBackPacket(item.ChatId, CallBackCode.AprovePaymentRequest, item.KeyValue)),
                new InlineButton("Назад", new CallBackPacket(CallBackCode.PaymentsList)))

        };
    }
}
