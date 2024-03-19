using FileDataBase.Main;
using FileDataBase.Types;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Types;
using System;

namespace MessageConverters;

[Serializable]
public class WishToMessageConverter : TelegramMessageConverter<Wish>
{
    public override string AsListItem(Wish item) => $"{item.KeyValue}, {item.RecordTime:G}";

    public override TelegramMessage ConvertCollection(TelegramUser user) => MessageCreator.CreateMessageList<Wish, int>(user, DataBase.Wishes.Items);
    public override TelegramMessage ConvertItem(TelegramUser user, Wish item)
    {
        TelegramMessage message = new TextMessage(user?.KeyValue)
        {
            Text = $"От клиента: (чат Id) : {item.ChatId}\nИмя: {item.UserName}\n{item.Text}"
        };

        if (item.HasAnswered)
        {
            message.Markup = new InlineMarkup(
                    new InlineButton("К списку желаний", new CallBackPacket(CallBackCode.WishesList)));
        }
        else
            message.Markup = new InlineMarkup(
                    new InlineButton("Ответить на желание", new CallBackPacket(item.ChatId, CallBackCode.AnswerWish, item.KeyValue)),
                    new InlineButton("Желание некорректно", new CallBackPacket(item.ChatId, CallBackCode.DenyWish, item.KeyValue)),
                    new InlineButton("К списку желаний", new CallBackPacket(CallBackCode.WishesList)));
        return message;
    }
}