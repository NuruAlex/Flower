using FileDataBase.Types;
using Flower;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Dialogs.ClientDialogs;

[Serializable]
public class CheckClientSubscriptionProcess : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        ChatMember member = await TelegramBot.Bot.GetChatMemberAsync(-1001610293273, User.Id);

        if (member.Status == ChatMemberStatus.Left || member.Status == ChatMemberStatus.Kicked)
            await Sender.SendMessage(User?.KeyValue, MessageArchieve.GetResourceMessage(ResourceMessageType.Subsctibe));
    }
}
