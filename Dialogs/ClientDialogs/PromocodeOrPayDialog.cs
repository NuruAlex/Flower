using FileDataBase.Types;
using Flower;
using Flower.Handlers;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Reply.Rows;
using Messages.Types;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dialogs.ClientDialogs;

[Serializable]
public class PromocodeOrPayDialog : IOneActProcess
{

    private IOneActProcess _dialog;

    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public void NextAction(Message message)
    {
        _dialog?.NextAction(message);
    }

    public async void NextAction(Message message, CallBackPacket packet)
    {
        await TelegramBot.Bot.DeleteMessageAsync(User.KeyValue, message.MessageId);

        if (packet.Code == CallBackCode.Continue)//continue?
        {
            if (PromocodeHandler.GetClientCurrentPromocode(User as Client) == null)
                await Sender.SendMessage(new TextMessage(User?.KeyValue)
                {
                    Text = "Итак, вы хотите дальше играть в чат боте, вы можете ввести промокод , который вам даст 2 дополнительных хода",
                    Markup = new InlineMarkup(new InlineButton("У меня есть промокод", new CallBackPacket(User.KeyValue, CallBackCode.BotPromocode)))
                });
            else
                await Sender.SendMessage(new TextMessage(User?.KeyValue)
                {
                    Text = "Продолжить игру можете в 3 форматах:\n\n" +
                   "1) также в телеграмм Боте можете пройти все 8 лепестков, ценность 1000 р\n" +
                   "2) онлайн игра в группе до 5 человек, ценность 4000 р, длительность 3-4 часа\n" +
                   "3) индивидуальная онлайн игра - длительность 3-4 часа, ценность 10 000 р. \n\n" +
               "Офлайн игры проводятся в Москве под запрос, также возможен выезд в другие города / страны под запрос. Выберете в каком формате хотите продолжить игру:",

                    Markup = new InlineMarkup(
                                new InlineButton("1) В чат боте", new CallBackPacket(User.KeyValue, CallBackCode.ChooseBotGame)), new InlineRow(),
                                new InlineButton("2) Онлайн в группе", new CallBackPacket(User.KeyValue, CallBackCode.ChooseGroupGame)), new InlineRow(),
                                new InlineButton("3) Индивидуально Онлайн", new CallBackPacket(User.KeyValue, CallBackCode.ChooseIndividualGame)))
                });
        }

        if (packet.Code == CallBackCode.BotPromocode)
        {
            ProcessHandler.Run(User, new PromocodeDialog());
        }

        if (packet.Code == CallBackCode.ChooseBotGame)//choose game
            await Sender.SendMessage(new TextMessage(User?.KeyValue)
            {
                Text = "Итак, вы хотите дальше играть в чат боте оплатите 1000р и вы сможете играть уже в полноценную версию, пройти все 8 лепестков",
                Markup = new InlineMarkup(
                     new InlineButton("Оплата", new CallBackPacket(User.KeyValue, CallBackCode.PayBot)),
                     new InlineButton("Назад", new CallBackPacket(User.KeyValue, CallBackCode.Continue)))
            });

        if (packet.Code == CallBackCode.ChooseGroupGame)//choose game
            await Sender.SendMessage(new TextMessage(User?.KeyValue)
            {
                Text = "Итак, вы хотите оплатить индивидуальную онлайн игру, вы можете ввести промокод , который вам даст вам скидку",
                Markup = new InlineMarkup(
                            new InlineButton("У меня есть промокод на скидку", new CallBackPacket(User.KeyValue, CallBackCode.PayGroup, 1)),
                            new InlineRow(),
                            new InlineButton("Оплата", new CallBackPacket(User.KeyValue, CallBackCode.PayGroup, 2)),
                            new InlineButton("Назад", new CallBackPacket(User.KeyValue, CallBackCode.Continue)))
            });

        if (packet.Code == CallBackCode.ChooseIndividualGame)//choose game
            await Sender.SendMessage(new TextMessage(User?.KeyValue)
            {
                Text = "Итак, вы хотите оплатить индивидуальную онлайн игру, вы можете ввести промокод , который вам даст вам скидку",
                Markup = new InlineMarkup(
                    new InlineButton("У меня есть промокод на скидку", new CallBackPacket(User.KeyValue, CallBackCode.PayIndividual, 1)),
                    new InlineRow(),
                    new InlineButton("Оплата", new CallBackPacket(User.KeyValue, CallBackCode.PayIndividual, 2)),
                    new InlineButton("Назад", new CallBackPacket(User.KeyValue, CallBackCode.Continue)))
            });

        if (packet.Code == CallBackCode.PayBot)//choose bot
        {
            _dialog = new PayDialog(PaymentType.Bot, false);
            _dialog.Start();
        }

        if (packet.Code == CallBackCode.PayIndividual)
        {
            if (packet.Number == 1)//promo
            {
                _dialog = new PayDialog(PaymentType.Individual, true);
                _dialog.Start();
            }
            if (packet.Number == 2)//pay
            {
                _dialog = new PayDialog(PaymentType.Individual, false);
                _dialog.Start();
            }
        }

        if (packet.Code == CallBackCode.PayGroup)
        {
            if (packet.Number == 1)//promo
            {
                _dialog = new PayDialog(PaymentType.Group, true);
                _dialog.Start();
            }
            if (packet.Number == 2)//pay
            {
                _dialog = new PayDialog(PaymentType.Group, false);
                _dialog.Start();
            }
        }


        ProcessHandler.Processec.Find(i => i.KeyValue == User?.KeyValue)?.Update();
        User.Update(); return;
    }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(User?.KeyValue)
        {
            Text = "Вы завершили тест драйв игры с бесплатными ходами.\nВам понравилась игра, хотели бы продолжить полноценную  версию? \n",
            Markup = new InlineMarkup(new InlineButton("Да", new CallBackPacket(User.KeyValue, CallBackCode.Continue)))
        });
    }
}
