using Garage.Contracts;

namespace Garage.Vehicles.Vehicles;

internal class Car : Vehicle
{
    public FuelType FuelType { get; set; }
    //public string FuelType { get; set; }

    public Car() { }

    public Car(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
    }

    public Car(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        FuelType fuelType) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        FuelType = fuelType;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        string input = Utilities.PromptUserForValidInput("Please enter the number fuel type (gas or diesel):",
            (s) => s == "gas" || s == "diesel",
            ui,
            "Please enter either gas or diesel"
            );
        switch (input)
        {
            case "gas":
                FuelType = FuelType.gas;
                break;
            case "diesel":
                FuelType = FuelType.diesel;
                break;
            default:
                ui.PrintMessage($"{input} is not a valid fuel type.");
                return;
        }
    }

    public override string ToString()
    {
        return base.ToString() + $" Fuel type: {FuelType}";
    }
}
