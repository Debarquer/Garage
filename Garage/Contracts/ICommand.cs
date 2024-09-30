namespace Garage.Contracts;

internal interface ICommand
{
    /// <summary>
    /// Prints the help related information to the ui.
    /// </summary>
    public void PrintHelp(IUI ui);

    /// <summary>
    /// Invokes the command.
    /// </summary>
    /// <param name="parameters"></param>
    public void Invoke(string[] parameters);

    /// <summary>
    /// Validates the command, for example by checking the number of passed parameters.
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns>If the command can be invoked properly.</returns>
    public bool ValidateCommand(string[]? parameters);

    /// <summary>
    /// Returns the name of the command.
    /// </summary>
    /// <returns>The name of the command</returns>
    public string GetName();
}
