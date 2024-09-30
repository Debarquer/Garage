using Garage.Contracts;
using Garage.Vehicles;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Numerics;

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
                   "printpattern",
                   "garage ?color ?numberOfWheels ?maxSpeed ?owner ?registration",
                   "Prints all vehicles in garage matching the pattern. Use the pattern type:value.",
                   PrintVehiclesMatchingPattern,
                   true
                ),
                new Command(
                   "add",
                   "garage",
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

        private void PrintVehiclesMatchingPattern(string[] parameters)
        {         
            string? color = null;
            int? numberOfWheels = null;
            int? maxSpeed = null;
            string? owner = null;
            string? registration = null;
            string? type = null;

            for(int i = 1; i < parameters.Length; i++)
            {
                string[] parameterSplit = parameters[i].Split(':');
                if (parameterSplit == null || parameterSplit.Length < 2)
                {
                    ui.PrintMessage("Invalid parameter");
                    return;
                }

                string paramterType = parameterSplit[0];
                string parameterValue = parameterSplit[1];
                switch (paramterType)
                {
                    case "color":
                        color = parameterValue;
                        break;
                    case "numberOfWheels":
                        if(int.TryParse(parameterValue, out _))
                        {
                            numberOfWheels = int.Parse(parameterValue);
                        }
                        else
                        {
                            ui.PrintMessage($"{paramterType}:({parameterValue}) is not a valid number.");
                        }
                        break;
                    case "maxSpeed":
                        if (int.TryParse(parameterValue, out _))
                        {
                            maxSpeed = int.Parse(parameterValue);
                        }
                        else
                        {
                            ui.PrintMessage($"{paramterType}:({parameterValue}) is not a valid number.");
                        }
                        break;
                    case "owner":
                        owner = parameterValue;
                        break;
                    case "registration":
                        registration = parameterValue;
                        break;
                    case "type":
                        if (ValidateVehicleType(parameterValue))
                        {
                            type = parameterValue;
                        }
                        else
                        {
                            ui.PrintMessage($"{paramterType}:({parameterValue}) is not a valid type.");
                        }
                        break;
                    default:
                        ui.PrintMessage($"{paramterType}:({parameterValue}) is not valid.");
                        break;
                }
            }

            //string type = Utilities.PromptUserForValidInput("Please enter the vehicle type (airplane, boat, bus, car or motorcycle):",
            //    (string s) =>
            //    {
            //        return s == "airplane" || s == "boat" || s == "bus" || s == "car" || s == "motorcycle";
            //    },
            //    ui,
            //    "Please enter airplane, boat, bus, car or motorcycle"
            //    );         

            //if (v != null)
            //{
            //    garageHandler.AddVehicle(v, parameters[0]);
            //}

            garageHandler.PrintVehiclesMatchingPattern(parameters[0], registration, color, numberOfWheels, maxSpeed, owner, type);
        }

        private bool ValidateVehicleType(string s)
        {
            return s == "airplane" || s == "boat" || s == "bus" || s == "car" || s == "motorcycle";
        }

        private void AddVehicle(string[] parameters)
        {
            string color = Utilities.PromptUserForValidString("Please enter a color:", ui);
            int numberOfWheels = Utilities.PromptUserForValidNumber("Please enter the number of wheels:", ui);
            int maxSpeed = Utilities.PromptUserForValidNumber("Please enter the max speed:", ui);
            string owner = Utilities.PromptUserForValidString("Please enter the owners first name:", ui);
            string registration = Utilities.PromptUserForValidInput("Please enter the registration:", (string s) => s.Length > 0, ui);

            string type = Utilities.PromptUserForValidInput("Please enter the vehicle type (airplane, boat, bus, car or motorcycle):",
                (string s) =>
                {
                    return s == "airplane" || s == "boat" || s == "bus" || s == "car" || s == "motorcycle";
                },
                ui,
                "Please enter airplane, boat, bus, car or motorcycle"
                );

            IVehicle v = null;
            switch (type.ToLower())
            {
                case "airplane":
                    int numberOfEngines = Utilities.PromptUserForValidNumber("Please enter the number of engines:", ui);
                    v = new Airplane(registration, color, numberOfWheels, maxSpeed, owner, numberOfEngines);
                    break;
                case "boat":
                    int length = Utilities.PromptUserForValidNumber("Please enter the length: ", ui);
                    v = new Boat(registration, color, numberOfWheels, maxSpeed, owner, length);
                    break;
                case "bus":
                    int numberOfSeats = Utilities.PromptUserForValidNumber("Please enter the number of seats:", ui);
                    v = new Bus(registration, color, numberOfWheels, maxSpeed, owner, numberOfSeats);
                    break;
                case "car":
                    string input = Utilities.PromptUserForValidInput("Please enter the number fuel type (gas or diesel):",
                        (string s) => s == "gas" || s == "diesel",
                        ui,
                        "Please enter either gas or diesel"
                        );
                    FuelType fuelType;
                    switch (input)
                    {
                        case "gas":
                            fuelType = FuelType.gas;
                            break;
                        case "diesel":
                            fuelType = FuelType.diesel;
                            break;
                        default:
                            ui.PrintMessage($"{input} is not a valid fuel type.");
                            return;
                    }
                    v = new Car(registration, color, numberOfWheels, maxSpeed, owner, fuelType);
                    break;
                case "motorcycle":
                    int cylinderVolume = Utilities.PromptUserForValidNumber("Please enter the cylinder volume:", ui);
                    v = new Motorcycle(registration, color, numberOfWheels, maxSpeed, owner, cylinderVolume);
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
            string registration = Utilities.PromptUserForValidString($"Please enter the registration:", ui);

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
