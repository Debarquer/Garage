namespace Garage.Contracts;

internal interface IUI
{
    /// <summary>
    /// Prints a message to the user.
    /// </summary>
    /// <param name="message"></param>
    public void PrintMessage(string message);
}
