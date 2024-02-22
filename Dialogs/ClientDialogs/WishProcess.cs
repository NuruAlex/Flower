using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Bot;
using Flower.Dialogs.Sub;
using Flower.Handlers;
using Flower.Resources;
using Flower.SupportClasses;
using Flower.SupportClasses.Senders;
using MessageConverters;
using Messages.Main;
using Messages.Reply.Markups;
using Messages.Types;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Dialogs.ClientDialogs;

[Serializable]
public class WishProcess : IMultiActProcess
{
    public int Iteration { get; set; } = -1;
    public TelegramUser User { get; set; }

    private readonly IsClientMadeItem<Wish> _subProcess;

    private readonly Wish _wish;
    private readonly int _number;

    private readonly List<IDataProcess> _dialogs;

    public WishProcess()
    {
        User = UserHandler.CurrentUser;

        _number = CallBackData.Packet.Number;

        _dialogs = new()
        {
             new TextDialog("any"),
             new TextDialog("бросить кубик")
             {
                 ErrorMessage = new TextMessage(User ?.KeyValue) { Text = "Кажется произошла ошибка, бросьте кубик" }
             }
        };

        _subProcess = new(_number, "Вы уже записали желание номер: ");

        _wish = new Wish(_number, User as Client);
    }

    public async void NextAction(Message message)
    {
        Iteration++;

        if (!_dialogs[Iteration].IsCorrectData(message))
        {
            await Sender.SendMessage(_dialogs[Iteration].ErrorMessage);
            Iteration--;
            return;
        }

        if (Iteration == 0)
        {
            _wish.Text = message.Text;

            await Sender.SendMessage(new TextMessage(User.KeyValue)
            {
                Text = "Бросьте кубик, чтобы выбрать ресурс",
                Markup = new ReplyMarkup().AddButton("Бросить кубик")
            });
        }

        if (Iteration == 1)
        {
            int result = await DiceSender.SendDiceAsync(User.KeyValue);


            await Sender.SendMessage(new PhotoMessage(User.KeyValue)
            {
                Media = new()
                {
                    Path = ResourceFiles.Resources[result],
                    Caption = "Этот ресурс поможет вам в достижении желаемого"
                }
            });

            _wish.ResourceTitle = ResourceFiles.ResourcesTitle[result];

            TelegramMessage wish = Converter.ConvertItem(UserHandler.CurrentUser, _wish);

            await Sender.SendMailing(wish, DataBase.Admins.GetAllKeys());

            DataBase.Wishes.Add(_wish);
            DataBase.Moves.Add(new WishMove(User as Client, PositionHandler.GetCurrentPosition(User as Client), _wish));

            await Sender.SendMessage(new TextMessage(User.KeyValue)
            {
                Text = "Желание отправлено, в течение 2 суток автор игры Ольга Еренко, озвучит ваше желание на правах Вселенной" +
                        "\nА сейчас кидай кубик и смотри, что тебе выпадет дальше",

                Markup = new ReplyMarkup().AddButton("Бросить кубик")
            });

            ProcessHandler.Run(User, new MoveProcess(User as Client));
        }
        User.Update();
    }


    public async void Start()
    {
        if (User as Client == null)
            ProcessHandler.StopProcess(User);

        await _subProcess.Start(User, null, "Запишите свое желание (текстом)");
    }
}
