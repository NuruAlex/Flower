using FileDataBase.Main;
using FileDataBase.Types;
using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Markups;
using Messages.Types;
using System;

namespace MessageConverters;

[Serializable]
public class PresentationToMessageConverter : TelegramMessageConverter<Presentation>
{
    public override string AsListItem(Presentation item) => item.UserName ?? item.ChatId.ToString();

    public override TelegramMessage ConvertCollection(TelegramUser user) => MessageCreator.CreateMessageList<Presentation, int>(user, DataBase.Presentations.Items);
    public override TelegramMessage ConvertItem(TelegramUser user, Presentation item)
    {
        if (item is VideoPresentation videoPresentation)
        {
            return new VideoNoteMessage(user?.KeyValue)
            {
                Media = new()
                {
                    Path = videoPresentation.Path,
                },
                Markup = new InlineMarkup(
                         new InlineButton("Ответить", new CallBackPacket(videoPresentation.ChatId, CallBackCode.AnswerPresentation, videoPresentation.KeyValue)),
                         new InlineButton("К списку презентаций", new CallBackPacket(CallBackCode.PresentationsList)))
            };
        }
        else
        {
            return new TextMessage(user?.KeyValue)
            {
                Text = $"Самопрезентация от клиента (chat id): {item.ChatId}\n" +
                                      $"Номер самопрезентации: {item.KeyValue}",
                Markup = new InlineMarkup(
                         new InlineButton("Ответить", new CallBackPacket(item.ChatId, CallBackCode.AnswerPresentation, item.KeyValue)),
                         new InlineButton("К списку презентаций", new CallBackPacket(CallBackCode.PresentationsList)))
            };
        }
    }
}
