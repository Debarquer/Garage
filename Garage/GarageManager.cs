using Garage.Contracts;

namespace Garage
{
    internal class GarageManager : CommandManager
    {
        private IUI ui;

        public GarageManager(IUI ui)
        {
            Name = "Garage";
            Aliases = ["garage", "g"];

            CommandList = new CommandList(new Command[] {
               new Command(
                   "help",
                   "?command_name",
                   "Displays command help information.",
                   Help
                ),
                new Command(
                   "test",
                   "",
                   "Prints debug test info.",
                   PrintTestInfo
                ),
            });

            this.ui = ui;
        }

        private void PrintTestInfo(string[] parameters) => ui.PrintMessage("GarageManager test");
        private void Help(string[] parameters) => CommandList.PrintHelp(parameters, ui);
    }
}
