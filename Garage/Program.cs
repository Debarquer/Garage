using Garage;
using Garage.Contracts;
using Garage.UserInput;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            IConfig config = Config.LoadConfig(Directories.ConfigPath);
            Directories.Configure(config);

            FlowControlManagerFactory.Create().ManageInput();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}