using Garage.Contracts;

namespace Garage.Vehicles.Vehicles;

internal class Motorcycle : Vehicle
{
    public int CylinderVolume { get; set; }

    public Motorcycle() { }

    public Motorcycle(string registration,
    string color,
    int numberOfWheels,
    int maxSpeed,
    string owner) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
    }

    public Motorcycle(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int cylinderVolume) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        CylinderVolume = cylinderVolume;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        CylinderVolume = Utilities.PromptUserForValidNumber("Please enter the cylinder volume:", ui);
    }

    public override string ToString()
    {
        return base.ToString() + $" Cylinder volume: {CylinderVolume}";
    }
}
