using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Handlers;
using Flower.Resources;
using Flower.SupportClasses;
using Flower.SupportClasses.Senders;
using Messages.Main;
using Messages.Reply.Markups;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.ClientDialogs;

[Serializable]
public class SelectLeafProcess : IOneActProcess
{
    private readonly IDataProcess _dialog;


    public TelegramUser User { get; set; }

    public SelectLeafProcess()
    {
        User = UserHandler.CurrentUser;
        _dialog = new TextDialog("бросить кубик");
    }

    public string GetText(int result)
    {
        string functionResult = TelegramTexts.LeafTexts[(int)PositionHandler.GetCurrentPosition(User as Client).Leaf];

        string answer = $"Ход {DataBase.Moves.FindByClient(User as Client) + 1}-й.\n" +
            $" Выпала цифра: {result}\n У вас {(User as Client).UnitsOfHapiness} единиц счастья\n";
        return answer + functionResult;
    }

    public void SetStartPosition(int result)
    {
        PositionHandler.SetCurrentPosition(User as Client, Flower.Resources.Flower.GetPosition(result, 0));

        DataBase.Moves.Add(new Move(User as Client, PositionHandler.GetCurrentPosition(User as Client)));

        User.Update();
    }

    public async void NextAction(Message parameter)//1 time
    {
        if (!_dialog.IsCorrectData(parameter))
        {
            await Sender.SendMessage(_dialog.ErrorMessage);
            return;
        }


        int result = await DiceSender.SendDiceAsync(User.KeyValue);

        SetStartPosition(result);

        await Sender.SendMessage(new TextMessage(User?.KeyValue)
        {
            Text = GetText(result),
            Markup = new ReplyMarkup().AddButton("Бросить кубик")
        });

        ProcessHandler.Run(User, new MoveProcess(User as Client));
    }

    public async void Start()
    {
        if (User == null || User as Client == null)
            return;

        await Sender.SendMessage(User?.KeyValue, "Нажмите кнопку, чтобы выбрать лепесток");
    }
}
