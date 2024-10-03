using Garage.Contracts;

namespace Garage
{
    public class ConsoleUI : IUI
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string GetString()
        {
            return Console.ReadLine();
        }
    }
}
