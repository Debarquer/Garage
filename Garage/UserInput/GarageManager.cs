using Garage.Contracts;
using Garage.Vehicles;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace Garage.UserInput;

internal class GarageManager : CommandManager
{
    private readonly IUI ui;
    private readonly GarageHandler<IVehicle> garageHandler;

    public GarageManager(IUI ui, IHandler<IVehicle> garageHandler)
    {
        Name = "Garage";
        Aliases = ["garage", "g"];

        CommandList = new CommandList([
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
               "?garage",
               "Prints all vehicle types in garage.",
               PrintTypes
            ),
            new Command(
               "printpatternall",
               "?type ?color ?numberOfWheels ?maxSpeed ?owner ?registration",
               "Prints all vehicles in all garages matching the pattern. Use the pattern type:value.",
               PrintAllVehiclesMatchingPattern,
               true
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
        ]);

        this.ui = ui;
        this.garageHandler = (GarageHandler<IVehicle>?)garageHandler;
    }

    #region Command methods
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
    private void PrintTypes(string[] parameters)
    {
        if(parameters.Length > 0)
        {
            garageHandler.PrintTypes(parameters[0]);
        }
        else
        {
            garageHandler.PrintAllTypes();
        }
    }

    private void PrintAllVehiclesMatchingPattern(string[] parameters) => garageHandler.PrintAllVehiclesMatchingPattern(parameters);
    private void PrintVehiclesMatchingPattern(string[] parameters) => garageHandler.PrintVehiclesMatchingPattern(parameters[0], parameters.Skip(1).ToArray());
    private void AddVehicle(string[] parameters)
    {
        string garageName = parameters[0];
        if (!garageHandler.HasGarage(garageName))
        {
            ui.PrintMessage("Invalid garage");
            return;
        }

        if(garageHandler.GetGarage(garageName).IsFull)
        {
            ui.PrintMessage("Garage is full");
            return;
        }

        string availableTypes = VehicleUtility.GetAvailableVehicleStrings();
        string typeName = Utilities.PromptUserForValidInput($"Please enter the vehicle type ({availableTypes}):",
            VehicleUtility.ValidateType,
            ui,
            $"Invalid type."
        );

        Type type = VehicleUtility.GetType(typeName);
        if (!VehicleUtility.IsIVehicle(type))
        {
            ui.PrintMessage(("Invalid type"));
            return;
        }

        var dataType = type.GetNestedTypes().Where(t => typeof(IVehicleData).IsAssignableFrom(t)).FirstOrDefault();
        IVehicleData data = dataType.InvokeMember(
            "GetData", 
            System.Reflection.BindingFlags.InvokeMethod, 
            null, 
            null,
            [ui, garageHandler, garageName]) as IVehicleData;

        IVehicle vehicle = (IVehicle?)Activator.CreateInstance(type, data);

        if (vehicle == null)
        {
            ui.PrintMessage("Failed to instantiate vehicle");
            return;
        }

        garageHandler.AddVehicle(vehicle, parameters[0]);
    }

    private void RemoveVehicle(string[] parameters)
    {
        string registration = Utilities.PromptUserForString($"Please enter the registration:", ui);

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
            garageHandler.Load(parameters[0]);
        }
    }
    #endregion
}
