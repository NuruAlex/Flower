using Flower.Bot.Resources;
using Flower.Resources;
using Flower.SupportClasses;
using Messages.Main;
using Messages.Reply.CallBack;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flower;

public static class TelegramBot
{
    public static string _token = "6894117707:AAFWp7dZcMrwUr1d74UR_xq4Q1eeEn90YBg";

    private static ITelegramBotClient _bot = null;
    public static ITelegramBotClient Bot => _bot ??= new TelegramBotClient(_token);


    public delegate void OnMessageResieved(Message message);
    public delegate void OnCallBackResieved(CallBackPacket packet, Message message);

    public static event OnCallBackResieved OnCallBack;
    public static event OnMessageResieved OnMessage;

    public static void Start() => Bot.StartReceiving(OnUpdate, OnError);

    private static async void Sender_OnSendError(Exception exception, string message) => await Sender.SendMessage(5082579517, $"{exception.Message} {message}");

    public static void DeleteAllMessages(List<int> ids) => ids.ForEach(async i => { await DeleteMessageForAdmin(i); });

    public static async Task DeleteMessageForAdmin(int id)
    {
        try
        {
            if (id != -1 && UserHandler.CurrentUser != null)
                await Bot.DeleteMessageAsync(UserHandler.CurrentUser.KeyValue, id);
        }
        catch (Exception ex)
        {
            Sender_OnSendError(ex, "TelegramBot / DeleteMessageForAdmin");
        }
    }


    public static async Task LoadAdminCommandsAsync()
    {
        List<BotCommand> commands = new();

        foreach (CommandGroup group in CommandArchieve.GetAdminCommands())
            foreach (AdminCommand command in group.Commands)
                commands.Add(new BotCommand
                {
                    Command = command.Command,
                    Description = command.Description
                });

        await Bot.SetMyCommandsAsync(commands);
    }

    public static async Task OnUpdate(ITelegramBotClient bot, Update updatef, CancellationToken token)
    {
        int offset = 0;

        while (true)
        {
            Update[] updates = await Bot.GetUpdatesAsync(offset);

            foreach (var update in updates)
            {
                offset = update.Id + 1;

                if (update.Type == UpdateType.CallbackQuery) // on callback button
                {
                    CallbackQuery query = update.CallbackQuery;

                    Message message = query.Message;

                    UserHandler.SetCurrentUser(message.Chat.Id);

                    CallBackPacket packet = new(query.Data);

                    UserHandler.SetCallBackUser(packet);

                    OnCallBack.Invoke(packet, message);
                }
                else if (update.Type == UpdateType.Message) // on message
                {
                    Message message = update.Message;

                    UserHandler.SetCurrentUser(message.Chat.Id, message.From.Id, message.From.Username);

                    OnMessage.Invoke(message);
                }
            }
        }
    }



    public static async Task OnError(ITelegramBotClient client, Exception exсeption, CancellationToken token) => await Sender.SendMessage(5082579517, $"{exсeption.Message} OnError");

}
