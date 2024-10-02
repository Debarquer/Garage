using Garage.Contracts;

namespace Garage.Vehicles.Vehicles;

internal class Boat : Vehicle
{
    public int Length { get; set; }

    public Boat() { }

    public Boat(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
    }

    public Boat(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int length) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Length = length;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        Length = Utilities.PromptUserForValidNumber("Please enter the length: ", ui);
    }

    public override string ToString()
    {
        return base.ToString() + $" Length: {Length}";
    }
}
