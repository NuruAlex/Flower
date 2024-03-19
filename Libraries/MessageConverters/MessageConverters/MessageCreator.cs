using CallBackPacketCreator;
using FileDataBase.Types;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Types;
using System.Collections.Generic;

namespace MessageConverters;

public static class MessageCreator
{
    public static TelegramMessage CreateMessageList<T, k>(TelegramUser user, List<T> items) where T : ClicableObject<k>
    {
        if (items == null || items.Count == 0)
        {
            return CreateEmptyMessageList(user);
        }
        else
        {
            return new TextMessage(user?.KeyValue)
            {
                Text = $"Количество объектов ({items.Count})",
                Markup = CreateDefaultInline<T, k>(items)
            };
        }
    }

    public static InlineMarkup CreateDefaultInline<T, k>(List<T> Items) where T : ClicableObject<k>
    {
        InlineMarkup buttons = new();

        AdminCallBackCreator<T> creator = AdminCallBackCreatorsArchieve.GetCreator<T>();

        for (int i = 0; i < Items.Count; ++i)
        {
            buttons.AddButton(
                Converter.ConvertToListItem(Items[i]),
                creator.CreatePacket(Items[i]));

            if (i % 2 == 0) buttons.AddRow();
        }

        buttons.AddRow();
        buttons.AddButton("Назад", new CallBackPacket(CallBackCode.GameData));
        return buttons;
    }

    public static TelegramMessage CreateEmptyMessageList(TelegramUser user) => new TextMessage(user?.KeyValue)
    {
        Text = "Кажется, список объектов пуст",
        Markup = new InlineMarkup(new InlineButton("Назад", new CallBackPacket(CallBackCode.GameData)))

    };
}
