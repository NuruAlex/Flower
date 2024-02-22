using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Types;
using System;

namespace Dialogs.ClientDialogs;

[Serializable]
public class ClientHelpProcess : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        MultiMessage message = new(User?.KeyValue, new()
        {
            new TextMessage()
            {
                Text = "Я помогу вам с вашей проблемой, если ваша проблема не относится к нижеперечисленному списку обращайтесь:\n" +
                                    "Автору игры: t.me/metoproducer или к разработчику: t.me/lekhaNuru\n\n1. Где найти кнопку?"
            },
            new PhotoMessage()
            {
                Media = new()
                {
                    Caption = "Если кнопка не появилась, найдите и нажмите выделенную кнопку, которая появилась у вас на клавиатуре",
                    Path = "Resources\\Help\\WhereIsButton.bmp"
                },
            },
            new TextMessage()
            {
                Text = "\n2. Что делать если кнопка пропала?\n\nЕсли кнопка \"Бросить кубик\" пропала, сообщите нам"
            }
        });


        await Sender.SendMultiMessage(message);
    }
}

