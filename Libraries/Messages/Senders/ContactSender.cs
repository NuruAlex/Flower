using Messages.Main;
using Messages.Types;
using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Messages.Senders;

[Serializable]
public class ContactSender : MessageSender
{
    public override async Task<int> SendMessage(TelegramMessage message)
    {
        if (message is not ContactMessage contactMessage || contactMessage.Contants == null) return -1;

        return await SendContacts(contactMessage.ChatId,
            contactMessage.Contants.FirstName,
            contactMessage.Contants.LastName,
            contactMessage.Contants.PhoneNumber,
            contactMessage.Contants.Price);
    }
    private async Task<int> SendContacts(long chatId, string firstName, string lastName, string prhone, double price)
    {
        await SenderClient.Bot.SendContactAsync(
        chatId: chatId,
        phoneNumber: prhone,
        firstName: firstName,
        lastName: lastName);

        TelegramMessage message = new TextMessage(chatId)
        {
            Text = $"Скопируйте номер телефона и переведите на него {price} рублей (Тинькофф или Сбербанк)"
        };
        return await Sender.SendMessage(message);
    }
}
