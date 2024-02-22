using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Handlers;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Reply.Markups;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.ClientDialogs;

[Serializable]
public class PromocodeDialog : IOneActProcess
{
    private readonly IDataProcess _dialog;

    public TelegramUser User { get; set; }

    public PromocodeDialog()
    {
        User = UserHandler.CurrentUser;

        _dialog = new TextDialog("any")
        {
            ErrorMessage = new TextMessage(User?.KeyValue) { Text = "Ожидается промокод" }
        };
    }

    public async void NextAction(Message parameter)
    {
        if (!_dialog.IsCorrectData(parameter))
        {
            await Sender.SendMessage(_dialog.ErrorMessage);
            return;
        }

        PromoCode promoCode = DataBase.PromoCodes.FindKey(parameter.Text.ToLower());

        if (promoCode == null)
        {
            await Sender.SendMessage(new TextMessage(User?.KeyValue)
            {
                Text = "Такого промокода не существует"
            });

            return;
        }

        promoCode.NumberOfUsing++;
        promoCode.Update();

        int additionallyMoves = 2;

        if (promoCode is SpecialPromoCode code)
            additionallyMoves = code.AddictionalyMoves;

        PromocodeHandler.SetClientCurrentPromocode(User as Client, promoCode);

        (User as Client).RemainingMoves += additionallyMoves;


        await Sender.SendMessage(new TextMessage(User?.KeyValue)
        {
            Text = $"Промокод успешно применен, вы получаете +{additionallyMoves} дополнительных хода",
            Markup = new ReplyMarkup().AddButton("Бросить кубик")
        });

        ProcessHandler.Run(User, new MoveProcess(User as Client));
    }

    public async void Start() => await Sender.SendMessage(User?.KeyValue, "Ждем промокод:");
}
