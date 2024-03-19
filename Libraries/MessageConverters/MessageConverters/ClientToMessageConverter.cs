using FileDataBase.Main;
using FileDataBase.Types;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Reply.Rows;
using Messages.Types;
using System;

namespace MessageConverters;

[Serializable]
public class ClientToMessageConverter : TelegramMessageConverter<Client>
{
    public override string AsListItem(Client item) => item.UserName ?? item.KeyValue.ToString();

    public override TelegramMessage ConvertCollection(TelegramUser user) => MessageCreator.CreateMessageList<Client, long>(user, DataBase.Clients.Items);


    public override TelegramMessage ConvertItem(TelegramUser user, Client item) => new TextMessage(user?.KeyValue)
    {
        Text = "Клиент: " + AsListItem(item),
        Markup = new InlineMarkup(
            new InlineButton("Сделать администратором", new CallBackPacket(item.KeyValue, CallBackCode.MakeNewAdmin)),
            new InlineRow(),
            new InlineButton("Статистика", new CallBackPacket(item.KeyValue, CallBackCode.ClientStatistics)),
            new InlineRow(),
            new InlineButton("Сводка", new CallBackPacket(item.KeyValue, CallBackCode.ClientSummary)),
            new InlineButton("Написать", new CallBackPacket(item.KeyValue, CallBackCode.WriteToClient)),
            new InlineRow(),
            new InlineButton("Подробнее...", new CallBackPacket(item.KeyValue, CallBackCode.MoreDetailes)),
            new InlineRow(),
            new InlineButton("Удалить", new CallBackPacket(item.KeyValue, CallBackCode.DeleteClient)),
            new InlineRow(),
            new InlineButton("Назад к списку", new CallBackPacket(item.KeyValue, CallBackCode.ClientsList)))
    };
}
