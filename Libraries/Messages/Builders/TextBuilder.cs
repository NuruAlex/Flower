using Messages.Types;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Messages.Builders;

[Serializable]
public class TextBuilder : MessageBuilder
{
    private TelegramMessage GetMessage(Message message) => new TextMessage()
    {
        Text = message.Text ?? "Unknown Text"
    };

    public override async Task<TelegramMessage> CreateTelegramMessage(Message message) => await Task.Run(() => GetMessage(message));

}
