using Garage.UserInput;

internal class Program
{
    private static void Main(string[] args)
    {
        FlowControlManagerFactory.Create().ManageInput();
    }
}