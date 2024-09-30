using Garage.Contracts;

namespace Garage;

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
            "help", 
            "Prints help information.",
            PrintHelp
            ),
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
        Console.WriteLine("Welcome to the program.");
        Console.WriteLine("Navigate the meny by typing in commands. Enter help for commands");// or help [command] for command specific documentation.");

        string input = string.Empty;
        while (input != "quit" && input != "exit")
        {
            input = Console.ReadLine();

            var inputSplit = input.Split(' ');

            if (inputSplit == null || inputSplit.Length == 0)
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            string commandDestination = inputSplit[0].ToLower();

            commandList.HandleInput(commandDestination, inputSplit, ui);
        }
    }

    private void PrintHelp(string[] p) => commandList.PrintHelp(p.Skip(1).ToArray(), ui);
    private void Route(string[] p)
    {
        CommandManager manager = managers
            .Where(x => x.Aliases.Contains(p[0]))
            .FirstOrDefault();

        if (manager != null)
        {
            manager.HandleInput(p[1], p.Skip(2).ToArray(), ui);
        }
        else
        {
            ui.PrintMessage($"{p[0]} is not a valid command.");
        }
    }
}
