using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower;
using Flower.Handlers;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot.Types;

namespace Dialogs.AdminDialogs;

[Serializable]
public class NewSpecialPromocodeDialog : IMultiActProcess
{
    private SpecialPromoCode promoCode = null;

    private readonly List<IDataProcess> _dialogs;
    private readonly List<int> _messages = new();


    public int Iteration { get; set; } = -1;
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public NewSpecialPromocodeDialog()
    {
        _dialogs = new()
        {
            new PromoProcess
            {
                IsNewPromo = true,
            },
            new NumberDialog(0, 5)
            {
                ErrorMessage = new TextMessage(UserHandler.CurrentUser ?.KeyValue) { Text = "Кажется произошла ошибка" }
            }
        };
    }

    public void DeleteAll()
    {
        Thread.Sleep(1000);
        TelegramBot.DeleteAllMessages(_messages);
        ProcessHandler.Run(User, new PrintListProcess<PromoCode, string>());
    }
    public async void NextAction(Message parameter)
    {
        _messages.Add(parameter.MessageId);

        Iteration++;
        if (!_dialogs[Iteration].IsCorrectData(parameter))
        {
            Iteration--;
            _messages.Add(await Sender.SendMessage(_dialogs[Iteration].ErrorMessage));
            DeleteAll();
            return;
        }
        if (Iteration == 0)
        {
            promoCode = new SpecialPromoCode(parameter.Text.ToLower());

            _messages.Add(await Sender.SendMessage(User?.KeyValue, "Введите количество дополнительных ходов (от 0 до 5):"));
        }
        if (Iteration == 1)
        {
            promoCode.AddictionalyMoves = (int)((NumberDialog)_dialogs[Iteration]).Result;

            DataBase.PromoCodes.Add(promoCode);
            _messages.Add(await Sender.SendMessage(User?.KeyValue, "Промокод добавлен"));
            DeleteAll();
        }
    }

    public async void Start()
    {
        _messages.Add(await Sender.SendMessage(User?.KeyValue, "Введите новый промокод: "));
        UserHandler.CurrentUser.Update();
    }
}
