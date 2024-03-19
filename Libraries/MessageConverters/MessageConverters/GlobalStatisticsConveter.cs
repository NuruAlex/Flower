using FileDataBase.Types;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Types;
using System;

namespace MessageConverters;

internal class GlobalStatisticsConveter : TelegramMessageConverter<GlobalStatistics>
{
    public override string AsListItem(GlobalStatistics item)
    {
        throw new NotImplementedException();
    }

    public override TelegramMessage ConvertCollection(TelegramUser user)
    {
        throw new NotImplementedException();
    }

    public override TelegramMessage ConvertItem(TelegramUser user, GlobalStatistics item)
    {
        string message = "";
        message += $"Статистика на {item.KeyValue}:\n";
        message += $"Количество клиентов: {item.ClientsAmount}\n";


        message += $"Количество клиентов, использовавших промокод: {item.UsedPromocodes}\n";


        foreach (PromoCode code in item.NumberOfUsing.Keys)
            message += $"    {code.KeyValue} - {item.NumberOfUsing[code]} клиентов\n";

        message += $"Среднее количество ходов: {item.AverageNumberOfMoves}\n\n";

        message += $"Количество клиентов которые оплатили: {item.AmountOfPayedClients}:\n";

        message += $"    бота оплатили: {item.AmountOfBotPayment}:\n\n";

        message += $"    групповую игру  оплатили:  {item.AmountOfGroupPayment}\n\n";

        message += $"    индивидуальную игру  оплатили: {item.AmountOfIndividualPayment}\n\n";


        return new TextMessage(user?.KeyValue)
        {
            Text = message,
            Markup = new InlineMarkup(new InlineButton("Назад к данным об игре", new CallBackPacket(CallBackCode.GameData)))

        };
    }
}
