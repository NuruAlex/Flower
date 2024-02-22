using Dialogs;

namespace Flower.Resources;


public abstract class MenuCommand
{
    public readonly string Command;
    public readonly IStartProcess Process;

    public MenuCommand(string command, IStartProcess process)
    {
        Command = command;
        Process = process;
    }
}

public class AdminCommand : MenuCommand
{
    public readonly string Description;

    public AdminCommand(string command, string description, IStartProcess process) : base(command, process) => Description = description;
}

public class ClientCommand : MenuCommand
{
    public ClientCommand(string command, IStartProcess process) : base(command, process) { }
}
