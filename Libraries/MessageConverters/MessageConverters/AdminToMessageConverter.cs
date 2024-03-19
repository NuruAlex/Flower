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
public class AdminToMessageConverter : TelegramMessageConverter<Admin>
{
    public override string AsListItem(Admin item) => item.UserName ?? item.KeyValue.ToString();

    public override TelegramMessage ConvertCollection(TelegramUser user) => MessageCreator.CreateMessageList<Admin, long>(user, DataBase.Admins.Items);

    public override TelegramMessage ConvertItem(TelegramUser user, Admin item) => new TextMessage(user?.KeyValue)
    {
        Text = $"Админ: {item.KeyValue}, {item.UserName}",
        Markup = new InlineMarkup(
            new InlineButton("Удалить", new CallBackPacket(item.KeyValue, CallBackCode.DeleteAdmin)),
            new InlineRow(),
            new InlineButton("Назад к списку", new CallBackPacket(item.KeyValue, CallBackCode.AdminsList)))
    };

}