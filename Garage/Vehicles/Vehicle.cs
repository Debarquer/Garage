using Garage.Contracts;

namespace Garage.Vehicles;

internal abstract class Vehicle : IVehicle
{
    public string Registration { get; set; }

    public string Color { get; set; }

    public int NumberOfWheels { get; set; }

    public int MaxSpeed { get; set; }

    public string Owner { get; set; }

    public Vehicle() { }

    public Vehicle(
        string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) 
    {  
        Registration = registration;
        Color = color;
        NumberOfWheels = numberOfWheels;
        MaxSpeed = maxSpeed;
        Owner = owner;
    }

    public abstract void PromptUserForAdditionalData(IUI ui);

    public override string ToString()
    {
        return $"{Registration}: {Color} {NumberOfWheels} wheels Max {MaxSpeed}kmph Owner: {Owner}";
    }
}
