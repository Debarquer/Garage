using Garage.Contracts;
using Garage.Vehicles;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Garage.UserInput;

internal class GarageManager : CommandManager
{
    private IUI ui;
    private GarageHandler<IVehicle> garageHandler;

    public GarageManager(IUI ui, IHandler<IVehicle> garageHandler)
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
               "print",
               "?garage",
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
               "garage ?type ?color ?numberOfWheels ?maxSpeed ?owner ?registration",
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
               "cleargarage",
               "garage",
               "Empties a garage.",
               EmptyGarage
            ),
            new Command(
               "printgarages",
               "",
               "Prints all available garages.",
               PrintGarages
            ),
            new Command(
               "save",
               "?path",
               "Saves all garages.",
               Save
            ),
            new Command(
               "load",
               "?path",
               "Loads all garages.",
               Load
            ),
        });

        this.ui = ui;
        this.garageHandler = (GarageHandler<IVehicle>?)garageHandler;
    }

    #region Commands
    private void Help(string[] parameters) => CommandList.PrintHelp(parameters, ui);
    private void PrintAllVehicles(string[] parameters)
    {
        if(parameters.Length > 0)
        {
            garageHandler.PrintAllVehicles(parameters[0]);
        }
        else
        {
            garageHandler.PrintAllVehicles();
        }
    }
    private void PrintTypes(string[] parameters) => garageHandler.PrintTypes(parameters[0]);
    private void PrintVehiclesMatchingPattern(string[] parameters) => garageHandler.PrintVehiclesMatchingPattern(parameters[0], parameters.Skip(1).ToArray());
    private void AddVehicle(string[] parameters)
    {
        if (!garageHandler.HasGarage(parameters[0]))
        {
            ui.PrintMessage("Invalid garage");
            return;
        }

        string color = Utilities.PromptUserForValidString("Please enter a color:", ui);
        int numberOfWheels = Utilities.PromptUserForValidNumber("Please enter the number of wheels:", ui);
        int maxSpeed = Utilities.PromptUserForValidNumber("Please enter the max speed:", ui);
        string owner = Utilities.PromptUserForValidString("Please enter the owners first name:", ui);
        string registration = Utilities.PromptUserForValidInput("Please enter the registration:", (string s) => s.Length > 0, ui);

        IEnumerable<Type> types = Assembly.Load("Garage").GetTypes().Where(t => t.Namespace == "Garage.Vehicles.Vehicles");
        string availableTypes = "";
        foreach (var availableType in types)
        {
            if (availableType.Name.ToLower() != "<>c")
                availableTypes += availableType.Name.ToLower() + ", ";
        }
        string typeName = Utilities.PromptUserForValidInput($"Please enter the vehicle type ({availableTypes}):",
            (string s) =>
            {
                foreach(Type? type in types)
                {
                    var typeNameLower = type.Name.ToLower();
                    if(s.ToLower() == typeNameLower)
                    {
                        return true;
                    }
                }
                return false;
            },
            ui,
            $"Invalid type."
            );

        IVehicle v = null;

        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
        typeName = myTI.ToTitleCase(typeName);
        Type type = Assembly.Load("Garage").GetTypes().First(t => t.Name == typeName);

        TypeFilter typeFilter = new TypeFilter((Type typeObj, Object criteria) =>
        {
            return typeObj.ToString() == criteria.ToString();
        });

        bool isIVehicle = type.IsAssignableTo(typeof(IVehicle));
        if (isIVehicle)
        {
            v = (IVehicle?)Activator.CreateInstance(type, registration, color, numberOfWheels, maxSpeed, owner);
            v.PromptUserForAdditionalData(ui);
        }
        else
        {
            ui.PrintMessage(("Invalid type"));
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

    private void EmptyGarage(string[] parameters)
    {
        string input = Utilities.PromptUserForValidInput(
            $"You are about to remove all vehicles from garage {parameters[0]}. Are you sure? (yes/no)",
            (string s) => s == "yes" || s == "no", 
            ui);

        if(input == "yes")
        {
            garageHandler.ClearGarage(parameters[0]);
        }
        else if(input == "no")
        {
            ui.PrintMessage("Action cancelled");
        }
        else
        {
            ui.PrintMessage("Invalid input");
        }
    }
    private void PrintGarages(string[] parameters) => garageHandler.PrintGarages();
    private void Save(string[] parameters)
    {
        if(parameters.Length == 0)
        {
            garageHandler.SaveAll();
        }
        else if(parameters.Length == 1)
        {
            garageHandler.Save(parameters[0]);
        }
    }
    private void Load(string[] parameters)
    {
        if (parameters.Length == 0)
        {
            garageHandler.LoadAll();
        }
        else if (parameters.Length == 1)
        {
            garageHandler.Load(Path.Combine(Directories.SavePath, parameters[0]));
        }
    }
    #endregion
}
