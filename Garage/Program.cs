using Garage.UserInput;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            FlowControlManagerFactory.Create().ManageInput();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}