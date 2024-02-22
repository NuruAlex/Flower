using Flower.Resources;
using System.Collections.Generic;

namespace Flower.Bot.Resources;

public class CommandGroup
{
    public readonly string Title;
    public readonly List<AdminCommand> Commands;
    public CommandGroup(string title, List<AdminCommand> commands)
    {
        Title = title;
        Commands = commands;
    }
}
