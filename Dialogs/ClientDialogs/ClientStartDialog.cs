using FileDataBase.Types;
using Flower.Handlers;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Reply.Markups;
using Messages.Types;
using System;

namespace Dialogs.ClientDialogs;

[Serializable]
public class ClientStartDialog : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        MultiMessage multiMessage = new(User?.KeyValue, new()
        {
            new TextMessage()
            {
                Markup = new ReplyMarkup().AddButton("Бросить кубик"),
                Text = TelegramTexts.Grettings
            },
            new PhotoMessage()
            {
                Media = new()
                {
                    Path = ResourceFiles.Field,
                    Caption = TelegramTexts.GameDescription
                }
            }
        });

        await Sender.SendMultiMessage(multiMessage);
        ProcessHandler.Run(User, new SelectLeafProcess());
    }
}
