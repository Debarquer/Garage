using Garage.UserInput;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        FlowControlManagerFactory.Create().ManageInput();
    }
}