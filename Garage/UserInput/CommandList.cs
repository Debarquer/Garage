using Garage.Contracts;

namespace Garage.UserInput;

internal class CommandList : ICommandList
{
    public ICommand[] Commands { get; }
    private readonly ICommand defaultCommand;

    public CommandList(ICommand[] commands, ICommand defaultCommand = null)
    {
        Commands = commands;
        this.defaultCommand = defaultCommand;
    }

    public void PrintHelp(string[] parameters, IUI ui)
    {
        int numberOfParameters = parameters.Length;

        if (numberOfParameters == 0)
        {
            ui.PrintMessage($"Help:");
            ui.PrintMessage($"Input command separated by spaces.");
            string commands = "";
            foreach (Command command in Commands)
            {
                commands += $"{command.GetName()}, ";
            }
            ui.PrintMessage($"Available subcommands: {commands}");
            ui.PrintMessage($"Enter the subcommand followed by the help for a more detailed description and their parameters.");
        }
        else if (numberOfParameters == 1)
        {
            ICommand command = Commands.Where(x => x.GetName() == parameters[0]).FirstOrDefault()!;
            if (command == null)
            {
                ui.PrintMessage($"Invalid command: {parameters[0]}");
                return;
            }

            command.PrintHelp(ui);
        }
        else
        {
            ui.PrintMessage("Invalid input: Help requires 2 or 3 parameters");
            return;
        }
    }

    public void HandleInput(string inputCommand, string[]? parameters, IUI ui)
    {
        ICommand command = Commands.Where(x => x.GetName() == inputCommand).FirstOrDefault()!;
        if (command == null)
        {
            if (defaultCommand != null)
            {
                defaultCommand.Invoke(parameters);
                return;
            }
            ui.PrintMessage($"Invalid command: {inputCommand}");
            return;
        }

        if (!command.ValidateCommand(parameters)) return;

        command.Invoke(parameters!);
    }
}
