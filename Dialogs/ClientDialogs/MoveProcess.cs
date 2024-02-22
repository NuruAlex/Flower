using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Handlers;
using Flower.Resources;
using Flower.SupportClasses;
using Flower.SupportClasses.Senders;
using Messages.Main;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Reply.Rows;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.ClientDialogs;

[Serializable]
public class MoveProcess : IOneActProcess
{
    private readonly IDataProcess _dialog;
    private readonly Client _client;

    public TelegramUser User { get; set; }

    public MoveProcess(Client client)
    {
        _client = client;
        _dialog = new TextDialog("Бросить кубик");
    }


    public string GetResultText(int result)
    {
        int numberOfMoves = DataBase.Moves.FindByClient(_client.KeyValue) + 1;

        string answer = $"Ход {numberOfMoves}-й.\n" +
           $"Выпала цифра: {result}\n";

        Position position = PositionHandler.GetCurrentPosition(_client);

        return answer + TelegramTexts.GetMoveText((int)position.Leaf, position.Cell);
    }

    public IMarkup GetMarkup(int cell)
    {
        IMarkup markup = new ReplyMarkup().AddButton("Бросить кубик");

        if (cell == 6 || cell == 10)
        {
            int number = Creator.GetChoiceNumber();

            markup = new InlineMarkup(
                 new InlineButton("Про деньги", new CallBackPacket(_client.KeyValue, CallBackCode.CreateChoice, number, "про деньги")), new InlineRow(),
                 new InlineButton("Ответы внутри себя", new CallBackPacket(_client.KeyValue, CallBackCode.CreateChoice, number, "ответы внутри себя")), new InlineRow(),
                 new InlineButton("Метафорические карты", new CallBackPacket(_client.KeyValue, CallBackCode.CreateChoice, number, "метафорические карты")));
        }
        else if (cell == 4 || cell == 9 || cell == 13)
            markup = new InlineMarkup(new InlineButton("Записать желание", new CallBackPacket(_client.KeyValue, CallBackCode.CreateWish, Creator.GetWishNumber())));
        else if (cell == 8)
            markup = new InlineMarkup(new InlineButton("Записать самопрезентацию", new CallBackPacket(_client.KeyValue, CallBackCode.CreatePresentation, Creator.GetPresentationNumber())));

        return markup;
    }

    public async void NextAction(Message message)
    {
        if (!_dialog.IsCorrectData(message))
        {
            await Sender.SendMessage(_dialog.ErrorMessage);
            return;
        }

        int result = await DiceSender.SendDiceAsync(_client.KeyValue);

        GameMovingHandler.MoveClient(_client, result);
        GameMovingHandler.OnBonusCell(_client);

        IMarkup markup = GetMarkup(PositionHandler.GetCurrentPosition(_client).Cell);

        if (markup is ReplyMarkup)
            DataBase.Moves.Add(new Move(_client, PositionHandler.GetCurrentPosition(User as Client)));


        await Sender.SendMessage(new TextMessage(_client?.KeyValue)
        {
            Text = GetResultText(result),
            Markup = markup
        });
    }

    public void Start()
    {
    }
}
