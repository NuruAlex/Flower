using Messages.Main;
using Messages.Types;
using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Messages.Senders;

[Serializable]
public class TextSender : MessageSender
{
    public override async Task<int> SendMessage(TelegramMessage data)
        => (await SenderClient.Bot.SendTextMessageAsync(chatId: data.ChatId, text: (data as TextMessage).Text, replyMarkup: data.Markup.ToMarkUp())).MessageId;
}
