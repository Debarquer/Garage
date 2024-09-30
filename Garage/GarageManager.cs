using Garage.Contracts;

namespace Garage
{
    internal class GarageManager : CommandManager
    {
        private IUI ui;
        private GarageHandler<IVehicle> garageHandler;

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
                new Command(
                   "print",
                   "garage",
                   "Prints all vehicles in garage.",
                   PrintAllVehicles
                ),
                new Command(
                   "printtypes",
                   "garage",
                   "Prints all vehicle types in garage.",
                   PrintTypes
                ),
                new Command(
                   "add",
                   "garage type registration",
                   "Adds a vehicle to the garage. Only unique registrations are allowed.",
                   AddVehicle
                ),
                new Command(
                   "remove",
                   "garage registration",
                   "Removes a veicle from the garage.",
                   RemoveVehicle
                ),
            });

            this.ui = ui;

            garageHandler = new GarageHandler<IVehicle>(100, ui);
        }

        private void Help(string[] parameters) => CommandList.PrintHelp(parameters, ui);
        private void PrintTestInfo(string[] parameters) => ui.PrintMessage("GarageManager test");
        private void PrintAllVehicles(string[] parameters) => garageHandler.PrintAllVehicles(parameters[0]);
        private void PrintTypes(string[] parameters) => garageHandler.PrintTypes(parameters[0]);
        private void AddVehicle(string[] parameters)
        {
            Vehicle v = null;
            switch (parameters[1].ToLower())
            {
                case "car":
                    v = new Car(parameters[2]);
                    break;
                case "boat":
                    v = new Boat(parameters[2]);
                    break;
                default:
                    ui.PrintMessage($"AddVehicle error: {parameters[1]} is not a valid vehicle type.");
                    return;
            }

            if(v != null)
            {
                garageHandler.AddVehicle(v, parameters[0]);
            }
        }
        private void RemoveVehicle(string[] parameters) => garageHandler.RemoveVehicle(parameters[1], parameters[0]);
    }
}
