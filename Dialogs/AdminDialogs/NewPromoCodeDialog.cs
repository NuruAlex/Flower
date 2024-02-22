using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower;
using Flower.Handlers;
using Flower.SupportClasses;
using Messages.Main;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot.Types;

namespace Dialogs.AdminDialogs;

[Serializable]
public class NewPromoCodeDialog : IOneActProcess
{
    private readonly PromoProcess _dialog = new()
    {
        IsNewPromo = true,
    };
    private readonly List<int> _messages = new();

    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public void DeleteAll()
    {
        Thread.Sleep(1000);
        TelegramBot.DeleteAllMessages(_messages);
        ProcessHandler.Run(User, new PrintListProcess<PromoCode, string>());
    }

    public async void NextAction(Message message)
    {
        _messages.Add(message.MessageId);

        if (!_dialog.IsCorrectData(message))
        {
            _messages.Add(await Sender.SendMessage(_dialog.ErrorMessage));
            DeleteAll();
            return;
        }


        DataBase.PromoCodes.Add(new DefaultPromoCode(message.Text.ToLower()));
        _messages.Add(await Sender.SendMessage(User?.KeyValue, "Промокод добавлен"));
        DeleteAll();
    }

    public async void Start()
    {
        _messages.Add(await Sender.SendMessage(User?.KeyValue, "Введите новый промокод: "));
        UserHandler.CurrentUser.Update();
    }
}
