using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Bot;
using Flower.Dialogs.Sub;
using Flower.Handlers;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Reply.Markups;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.ClientDialogs;

[Serializable]
public class SelectCardProcess : IOneActProcess //6 10
{
    private readonly string _path;
    private readonly ISubProcess _subProcess;
    private readonly CardChoice _choice;
    private readonly NumberDialog _dialog;

    public TelegramUser User { get; set; }


    public async void NextAction(Message message)
    {
        if (!_dialog.IsCorrectData(message))
        {
            await Sender.SendMessage(_dialog.ErrorMessage);
            return;
        }

        _choice.CardNumber = (int)_dialog.Result;


        await Sender.SendMessage(new PhotoMessage(User?.KeyValue)
        {
            Media = new()
            {
                Path = $"{_path}\\{_dialog.Result}.PNG",
                Caption = "Ваша выбранная карточка, что вы видите глядя на эту карту? " +
                            "Она вам нравится или нет? Какие мысли у вас приходят глядя на эту карту? " +
                                "Ответьте себе на эти вопросы и как будете готовы, бросайте кубик"
            },
            Markup = new ReplyMarkup().AddButton("Бросить кубик")
        });

        DataBase.Choices.Add(_choice);
        DataBase.Moves.Add(new SelectingCardMove(User as Client, PositionHandler.GetCurrentPosition(User as Client), _choice));

        ProcessHandler.Run(User, new MoveProcess(User as Client));

    }

    public SelectCardProcess()
    {
        User = UserHandler.CallBackUser;
        _subProcess = new IsClientMadeItem<CardChoice>(CallBackData.Packet.Number, "Вы уже делали выбор карт номер:");

        _choice = new CardChoice(CallBackData.Packet.Number, User as Client)
        {
            TypeOfChoice = CallBackData.Packet.SendData
        };

        _path = ResourceFiles.GetCardsPathByChoiceType(CallBackData.Packet.SendData);
        _dialog = new (0, ResourceFiles.CardsCount[CallBackData.Packet.SendData]);
        _dialog.ErrorMessage = new TextMessage(User?.KeyValue) { Text = "Кажется произошла ошибка, число некоректно" };
    }

    public async void Start()
    {
        if (User as Client == null)
            ProcessHandler.StopProcess(User);

        await _subProcess.Start(User, null, $"Введите число от {_dialog.LowNumber} до {_dialog.HighNumber}");
    }
}

