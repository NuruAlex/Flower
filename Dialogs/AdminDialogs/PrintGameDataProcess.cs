using FileDataBase.Main;
using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Reply.Rows;
using Messages.Types;
using System;

namespace Dialogs.AdminDialogs;


[Serializable]
public class PrintGameDataProcess : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(User?.KeyValue)
        {
            Text = "Данные об игре",
            Markup = new InlineMarkup(
                new InlineButton($"Клиенты ({DataBase.Clients.Count})", new CallBackPacket(CallBackCode.ClientsList)),
                new InlineButton($"Промокоды ({DataBase.PromoCodes.Count})", new CallBackPacket(CallBackCode.PromoCodeList)), new InlineRow(),
                new InlineButton($"Желания ({DataBase.Wishes.CountOf(i => i.HasAnswered == false)})", new CallBackPacket(CallBackCode.WishesList)),
                new InlineButton($"Оплаты ({DataBase.Payments.CountOf(i => i.Aproved == false)})", new CallBackPacket(CallBackCode.PaymentsList)), new InlineRow(),
                new InlineButton($"Администраторы ({DataBase.Admins.Count})", new CallBackPacket(CallBackCode.AdminsList)),
                new InlineButton($"Презентации ({DataBase.Presentations.CountOf(i => i.HasAnswered == false)}", new CallBackPacket(CallBackCode.PresentationsList)), new InlineRow(),
                new InlineButton($"Глобальная статистика", new CallBackPacket(CallBackCode.GlobalStatistics)))
        });
    }
}
