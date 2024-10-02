using Garage.Contracts;

namespace Garage.Vehicles.Vehicles;

internal class Airplane : Vehicle
{
    public int NumberOfEngines { get; set; }

    public Airplane() { }

    public Airplane(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
    }


    public Airplane(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int numberOfEngines) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        NumberOfEngines = numberOfEngines;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        NumberOfEngines = Utilities.PromptUserForValidNumber("Please enter the number of engines:", ui);
    }

    public override string ToString()
    {
        return base.ToString() + $" Number of engines: {NumberOfEngines}";
    }
}
