using FileDataBase.Main;
using FileDataBase.Types;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Reply.Rows;
using Messages.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageConverters;

[Serializable]
public class PromoCodeToMessageConverter : TelegramMessageConverter<PromoCode>
{
    public override TelegramMessage ConvertCollection(TelegramUser user)
    {
        if (DataBase.PromoCodes.Items.Count == 0)
            return AsEmptyMessageList(user);
        return AsMessageList(user);
    }
    public TelegramMessage AsMessageList(TelegramUser user)
    {
        TelegramMessage message = new TextMessage(user?.KeyValue)
        {
            Text = $"Список промокодов ({DataBase.PromoCodes.Count})"
        };

        List<PromoCode> promoCodes = DataBase.PromoCodes.Items.FindAll(x => x is DefaultPromoCode);
        List<PromoCode> specialPromoCodes = DataBase.PromoCodes.Items.FindAll(x => x is SpecialPromoCode);
        List<PromoCode> discontPromoCodes = DataBase.PromoCodes.Items.FindAll(x => x is DiscontPromoCode);

        InlineMarkup markup = new();
        int i = 0;
        markup.AddButton($"Обычные промокды ({promoCodes.Count}) + ", new CallBackPacket(CallBackCode.AddPromoCode))
            .AddRow();

        foreach (PromoCode code in promoCodes.Cast<PromoCode>())
        {
            markup.AddButton(code.KeyValue, new CallBackPacket(code.KeyValue, CallBackCode.Promocode));

            if (i % 2 != 0)
                markup.AddRow();
            i++;
        }

        i = 0;
        markup.AddRow()
            .AddButton(new InlineButton($"Специальные промокоды ({specialPromoCodes.Count}) + ", new CallBackPacket(CallBackCode.AddSpecialPromoCode)))
            .AddRow();

        foreach (SpecialPromoCode code in specialPromoCodes.Cast<SpecialPromoCode>())
        {
            markup.AddButton(code.KeyValue, new CallBackPacket(code.KeyValue, CallBackCode.Promocode));

            if (i % 2 != 0)
                markup.AddRow();
            i++;
        }

        i = 0;
        markup.AddRow()
            .AddButton(new InlineButton($"Скидочные промокоды ({discontPromoCodes.Count}) + ", new CallBackPacket(CallBackCode.AddDiscontPromoCode)))
            .AddRow();

        foreach (DiscontPromoCode code in discontPromoCodes.Cast<DiscontPromoCode>())
        {
            markup.AddButton(code.KeyValue, new CallBackPacket(code.KeyValue, CallBackCode.Promocode));

            if (i % 2 != 0)
                markup.AddRow();
            i++;
        }

        markup.AddButton($"Назад", new CallBackPacket(CallBackCode.GameData))
            .AddRow();


        message.Markup = markup;
        return message;
    }
    public TelegramMessage AsEmptyMessageList(TelegramUser user)
    {
        return new TextMessage(user?.KeyValue)
        {
            Text = "Список промокодов пуст",
            Markup = new InlineMarkup(
            new InlineButton($"Обычные промокды (0) + ", new CallBackPacket(CallBackCode.AddPromoCode)),
            new InlineRow(),
            new InlineButton($"Специальные промокоды (0) + ", new CallBackPacket(CallBackCode.AddSpecialPromoCode)),
            new InlineRow(),
            new InlineButton($"Скидочные промокоды (0) + ", new CallBackPacket(CallBackCode.AddDiscontPromoCode)),
            new InlineRow(),
            new InlineButton($"Назад", new CallBackPacket(CallBackCode.GameData)))
        };
    }


    public override TelegramMessage ConvertItem(TelegramUser user, PromoCode item)
    {
        if (item is DiscontPromoCode promoCode)
        {
            return new TextMessage(user?.KeyValue)
            {
                Text = promoCode.KeyValue,
                Markup = new InlineMarkup(
                             new InlineButton("Удалить", new CallBackPacket(promoCode.KeyValue, CallBackCode.DeletePromoCode)),
                             new InlineButton("Назад к списку промокодов", new CallBackPacket(CallBackCode.PromoCodeList)),
                             new InlineRow(),
                             new InlineButton("Подробная статистика", new CallBackPacket(promoCode.KeyValue, CallBackCode.PromoCodeInfo)))
            };
        }
        else
        {
            return new TextMessage(user?.KeyValue)
            {
                Text = item.KeyValue,
                Markup = new InlineMarkup(
                            new InlineButton("Удалить", new CallBackPacket(item.KeyValue, CallBackCode.DeletePromoCode)),
                            new InlineButton("Назад к списку промокодов", new CallBackPacket(CallBackCode.PromoCodeList)))
            };
        }
    }

    public override string AsListItem(PromoCode item) => item.KeyValue;
}
