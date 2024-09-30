using Garage.Contracts;
using Garage.Vehicles;

namespace Garage.UserInput
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
                   "garage type",
                   "Adds a vehicle to the garage. Only unique registrations are allowed.",
                   AddVehicle
                ),
                new Command(
                   "remove",
                   "garage",
                   "Removes a veicle from the garage.",
                   RemoveVehicle
                ),
                new Command(
                   "addgarage",
                   "garage capacity",
                   "Adds a new garage.",
                   AddGarage
                ),
                new Command(
                   "printgarages",
                   "",
                   "Prints all available garages.",
                   PrintGarages
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
            ui.PrintMessage($"Please enter the registration:");
            string registration = Console.ReadLine();

            Vehicle v = null;
            switch (parameters[1].ToLower())
            {
                case "car":
                    v = new Car(registration);
                    break;
                case "boat":
                    v = new Boat(registration);
                    break;
                default:
                    ui.PrintMessage($"AddVehicle error: {parameters[1]} is not a valid vehicle type.");
                    return;
            }

            if (v != null)
            {
                garageHandler.AddVehicle(v, parameters[0]);
            }
        }

        private void RemoveVehicle(string[] parameters)
        {
            ui.PrintMessage($"Please enter the registration:");
            string registration = Console.ReadLine();

            garageHandler.RemoveVehicle(registration, parameters[0]);
        }

        private void AddGarage(string[] parameters)
        {
            int capacity = 0;
            if(!int.TryParse(parameters[1], out capacity))
            {
                ui.PrintMessage($"{parameters[1]} is not a valid number");
                return;
            }

            garageHandler.AddGarage(parameters[0], capacity);
        }

        private void PrintGarages(string[] parameters) => garageHandler.PrintGarages();
    }
}
