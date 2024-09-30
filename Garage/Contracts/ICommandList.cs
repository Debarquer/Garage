namespace Garage.Contracts;

internal interface ICommandList
{
    ICommand[] Commands { get; set; }

    /// <summary>
    /// Handles an input command and print the output to the ui.
    /// </summary>
    /// <param name="inputCommand"></param>
    /// <param name="parameters"></param>
    void HandleInput(string inputCommand, string[]? parameters, IUI ui);

    /// <summary>
    /// Prints the help information of all the commands in the list, or a specific command if the id is passed as a parameter.
    /// </summary>
    /// <param name="parameters"></param>
    void PrintHelp(string[] parameters);
}