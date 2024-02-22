using Dialogs;
using Dialogs.AdminDialogs;
using Dialogs.ClientDialogs;
using FileDataBase.Types;
using Flower.Bot.Resources;
using System.Collections.Generic;

namespace Flower.Resources;

public static class CommandArchieve
{
    private static List<CommandGroup> _adminCommands;
    private static List<ClientCommand> _clientCommands;
    public static List<CommandGroup> GetAdminCommands()
    {
        Update();
        return _adminCommands;
    }
    private static void Update()
    {
        _adminCommands = new()
        {
            new CommandGroup("Старт", new ()
            {
                new AdminCommand("/start", "Начать/Обновить список команд", new StartDialog()),
                new ("/game_data","Данные об игре", new PrintGameDataProcess())
            }),
            new CommandGroup("Управление клиентами", new List<AdminCommand>
            {
                new ("/clients_list","Получает список клиентов",  new PrintListProcess<Client,long>() ),
            }),
            new CommandGroup("Управление промокодами", new List<AdminCommand>
            {
                new ("/promocode_list", "Получает список промокод", new PrintListProcess < PromoCode, string >()),
            }),
            new CommandGroup("Управление администраторами", new List<AdminCommand>
            {
                new ("/admins_list", "Получает список админов", new PrintListProcess < Admin, long >()),
            }),
            new CommandGroup("Управление желаниями", new List<AdminCommand>
            {
                new ("/wishes_list", "Получает список неотвеченных желаний", new PrintListProcess < Wish, int >())
            }),
            new CommandGroup("Управление самопрезентациями", new List<AdminCommand>
            {
                new ("/presentations_list", "Получает список неотвеченных самопрезентаций", new PrintListProcess < Presentation, int >())
            }),
            new CommandGroup("Управление оплатами", new List<AdminCommand>
            {
                new ("/payments_list", "Получает список покупок клиентов", new PrintListProcess < Payment, int >())
            }),
            new CommandGroup("Рассылки", new List<AdminCommand>
            {
                new ("/mailing_for_unpayed", "Рассылка для тех, кто не оплатил", new MailingForUnpayedDialog()),
                new ("/mailing_for_all", "Рассылка для всех", new MailingForAll())
            }),
            new CommandGroup("Помощь", new List<AdminCommand>
            {
                new ("/help", "Получает список команд, с описанием", new HelpDialog())
            })
        };

        _clientCommands = new()
        {
            new ("/start",new ClientStartDialog()),
        };
    }

    public static List<string> GetAdminCommandsList()
    {
        Update();

        List<string> list = new();

        foreach (var commandGroup in _adminCommands)
            foreach (var command in commandGroup.Commands)
                list.Add(command.Command);

        return list;
    }
    public static IStartProcess GetClientProcess(string text)
    {
        Update();
        ClientCommand command = _clientCommands.Find(i => i.Command == text);

        if (command != null)
            return command.Process;
        else return new UnknownProcess();
    }

    public static IStartProcess GetAdminProcess(string text)
    {
        Update();
        AdminCommand command;

        foreach (CommandGroup group in _adminCommands)
        {
            command = group.Commands.Find(i => i.Command == text);

            if (command != null)
                return command.Process;
        }
        return new UnknownProcess();
    }

}
