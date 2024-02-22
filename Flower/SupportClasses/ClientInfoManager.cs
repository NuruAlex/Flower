using FileDataBase.Main;
using FileDataBase.Types;
using MessageConverters;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Types;
using System.Collections.Generic;

namespace Flower.SupportClasses;

public static class ClientInfoManager
{

    public static TelegramMessage Detailes(Client client) => new TextMessage(UserHandler.CurrentUser?.KeyValue)
    {
        Text = MoreDetailes(client),
        Markup = new InlineMarkup(new InlineButton("Назад к клиенту", new CallBackPacket(UserHandler.CallBackUser.KeyValue, CallBackCode.Client)))
    };

    private static string MoreDetailes(Client client)
    {
        string message = "";
        message += $"Id: {client.Id}\n";
        message += $"Chat Id: {client.KeyValue}\n";
        message += $"User Name: {client.UserName}\n";
        message += $"Дата регистрации: {client.RegistrationTime:G}\n";
        message += $"Количество желаний: {DataBase.Wishes.CountOf(i => i.ChatId == client.KeyValue)}\n";
        message += $"Количество самопрезентаций: {DataBase.Presentations.CountOf(i => i.ChatId == client.KeyValue)}\n";
        message += $"Количество выбранный метафорических карт: {DataBase.Choices.CountOf(i => i.ChatId == client.KeyValue)}\n";
        message += $"Оставшихся ходов: {MoveHandler.GetClientRemainingMovesCount(client)}\n";
        message += $"Единиц счастья: {client.UnitsOfHapiness}\n";
        message += $"Количество ходов: {DataBase.Moves.FindByClient(client.KeyValue)}\n";
        message += $"Количество покупок: {PaymentsHandler.GetClientPaymentsCount(client)}\n";
        return message;
    }

    private static string GetStatistics(Client client)
    {
        int i = 1;
        string history = "Клиент" + Converter.ConvertToListItem(client);

        List<Move> moves = DataBase.Moves.FindAsList(i => i.KeyValue == client.KeyValue);

        moves.ForEach(move => { history += $"Ход {i++}: {move.Position}"; });
        return history;
    }

    public static TelegramMessage Statistics(Client client) => new TextMessage(UserHandler.CurrentUser?.KeyValue)
    {
        Text = GetStatistics(client),
        Markup = new InlineMarkup(new InlineButton("Назад к клиенту", new CallBackPacket(UserHandler.CallBackUser.KeyValue, CallBackCode.Client)))
    };

    public static TelegramMessage Summary(Client client) => new TextMessage(UserHandler.CurrentUser?.KeyValue)
    {
        Text = GetMoves(client),
        Markup = new InlineMarkup(new InlineButton("Назад к клиенту", new CallBackPacket(UserHandler.CallBackUser.KeyValue, CallBackCode.Client)))
    };

    private static string GetMoves(Client client)
    {
        string history = "Клиент" + Converter.ConvertToListItem(client);
        DataBase.Moves.FindAsList(i => i.ChatId == client.KeyValue).ForEach(x => history += x);

        return history;
    }

    public static Position GetLastClientPosition(Client client)
    {
        List<Move> moves = DataBase.Moves.FindAsList(i => i.ChatId == client.KeyValue);

        return moves[moves.Count - 1].Position;
    }
}
