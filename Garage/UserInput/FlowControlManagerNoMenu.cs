using Garage.Contracts;

namespace Garage.UserInput;

internal class FlowControlManagerNoMenu : IFlowControlManager
{
    private CommandManager[] managers;
    private IUI ui;

    ICommandList commandList;

    public FlowControlManagerNoMenu(CommandManager[] managers, IUI ui, Action initialization)
    {
        this.managers = managers;
        this.ui = ui;
        initialization?.Invoke();

        commandList = new CommandList(new ICommand[]
        {
            new Command(
                "help",
                "help ?command",
                "Prints help information.",
                PrintHelp
            ),
            new Command(
                "quit",
                "",
                "Exits the application.",
                (string[] parameters) => {/*Environment.Exit(0)*/}
            )
        },
        defaultCommand: new Command(
            "router",
            "",
            "",
            Route
        ));
    }

    /// <summary>
    /// Function to print a menu and manage user input.
    /// </summary>
    public void ManageInput()
    {
        ui.PrintMessage("Welcome to the program.");
        ui.PrintMessage("Navigate the meny by typing in commands. Enter help for commands");// or help [command] for command specific documentation.");

        string input = string.Empty;
        while (input != "quit" && input != "exit")
        {
            input = Utilities.PromptUserForString("", ui);

            if(input == "quit" || input == "exit") 
            { 
                return; 
            }

            var inputSplit = input.Split(' ');

            if (inputSplit == null || inputSplit.Length == 0)
            {
                ui.PrintMessage("Invalid input.");
                continue;
            }

            string commandDestination = inputSplit[0].ToLower();

            commandList.HandleInput(commandDestination, inputSplit, ui);
        }
    }

    private void PrintHelp(string[] p)
    {
        commandList.PrintHelp(p.Skip(1).ToArray(), ui);
        ui.PrintMessage("Available modules:");
        foreach(CommandManager manager in managers)
        {
            ui.PrintMessage(manager.Name);
        }
        ui.PrintMessage("Type module_name help for more information");
    }

    private void Route(string[] p)
    {
        CommandManager manager = managers
            .Where(x => x.Aliases.Contains(p[0]))
            .FirstOrDefault();

        if (manager == null)
        {
            ui.PrintMessage($"{p[0]} is not a valid command.");
            return;
        }

        manager.HandleInput(p[1], p.Skip(2).ToArray(), ui);
    }
}
