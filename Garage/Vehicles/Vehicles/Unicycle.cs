using Garage.Contracts;

namespace Garage.Vehicles.Vehicles;

internal class Unicycle : Vehicle
{
    public int Height { get; set; }
    public Unicycle() { }

    public Unicycle(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
    }

    public Unicycle(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int height) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Height = height;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        Height = Utilities.PromptUserForValidNumber("Please enter the height:", ui);
    }
}
