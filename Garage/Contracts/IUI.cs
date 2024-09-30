namespace Garage.Contracts;

public interface IUI
{
    /// <summary>
    /// Prints a message to the user.
    /// </summary>
    /// <param name="message"></param>
    public void PrintMessage(string message);

    /// <summary>
    /// Gets a string from the user.
    /// </summary>
    /// <returns></returns>
    public string GetString();
}
