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
public class NewDiscontPromoCodeDialog : IMultiActProcess
{

    private DiscontPromoCode _promocode;
    private readonly List<int> _messages = new();
    private readonly List<IDataProcess> _dialogs;

    public NewDiscontPromoCodeDialog()
    {
        _dialogs = new()
        {
            new PromoProcess
            {
                IsNewPromo = true,
            },
            new NumberDialog(0,100)
            {
                ErrorMessage = new TextMessage(User?.KeyValue) { Text = "Проценты от 0 до 100" }
            }
        };
    }

    public int Iteration { get; set; } = -1;
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }


    public void DeleteAll()
    {
        Thread.Sleep(1000);
        TelegramBot.DeleteAllMessages(_messages);
        ProcessHandler.Run(User, new PrintListProcess<PromoCode, string>());
    }

    public async void NextAction(Message parameter)
    {
        Iteration++;

        _messages.Add(parameter.MessageId);

        if (!_dialogs[Iteration].IsCorrectData(parameter))
        {
            Iteration--;

            _messages.Add(await Sender.SendMessage(_dialogs[Iteration].ErrorMessage));

            DeleteAll();
            return;
        }

        if (Iteration == 0)
        {
            _promocode = new DiscontPromoCode(parameter.Text.ToLower());
            _messages.Add(await Sender.SendMessage(User?.KeyValue, "Введите % скидку, например \"15\""));
        }
        if (Iteration == 1)
        {
            _promocode.Discont = ((int)((NumberDialog)_dialogs[Iteration]).Result);
            DataBase.PromoCodes.Add(_promocode);

            _messages.Add(await Sender.SendMessage(User?.KeyValue, "Скидочный промокод добавлен"));
            DeleteAll();
            return;
        }
        User.Update();
    }

    public async void Start()
    {
        _messages.Add(await Sender.SendMessage(User?.KeyValue, "Введите новый скидочный промокод: "));
        User.Update();
    }
}
