using Messages.Reply.Markups;
using Messages.Types;
using System.Collections.Generic;

namespace Flower.Resources;

public enum ResourceMessageType
{
    IsInDevelopment,
    Subsctibe,
    WaitAprovePayment,
    NeedToPaymentPhoto,
    WaitFirstAprove,


    SendQuestionOfContinuation,

    //presentations
    PresentationAlreadyAnswered,
    WritePresentationAnswer,
    VideoPresentationBonus,
    PresentationSent
}

public static class MessageArchieve
{
    private static readonly Dictionary<ResourceMessageType, TelegramMessage> _messages = new()
    {
        { ResourceMessageType.IsInDevelopment, new TextMessage()        { Text = "Находится в разработке"        } },
        { ResourceMessageType.Subsctibe, new TextMessage()              { Text = "Подписывайся на канал https://t.me/metoproducer, чтобы быть в курсе новостей\nПосле этого напиши предыдущую команду заново"  } },
        { ResourceMessageType.WaitAprovePayment, new TextMessage()      { Text =  "Администратор еще не разрешил вам идти дальше"} },
        { ResourceMessageType.NeedToPaymentPhoto, new TextMessage()     { Text =  "Загрузите скриншот перевода!"} },
        { ResourceMessageType.WaitFirstAprove, new TextMessage()        { Text =  "Подождите когда администратор одобрит ваш перевод"} },

        { ResourceMessageType.PresentationAlreadyAnswered, new TextMessage()        { Text =  "Самопрезентация уже имеет ответ"} },
        { ResourceMessageType.WritePresentationAnswer, new TextMessage()        { Text =  "Запишите свой ответ на самопрезентацию:"} },
        { ResourceMessageType.VideoPresentationBonus, new TextMessage()        { Text =  "Вам полагается +500 единиц счастья за ваше видео"} },
        { ResourceMessageType.PresentationSent, new TextMessage()        
        { 
            Text =  "Самопрезентация отправлена",
            Markup = new ReplyMarkup().AddButton("Бросить кубик")
        } },
    };


    public static TelegramMessage GetResourceMessage(ResourceMessageType type)
    {
        if (_messages.TryGetValue(type, out _))
            return _messages[type];

        return new UnknownTelegramMessage();
    }

}
