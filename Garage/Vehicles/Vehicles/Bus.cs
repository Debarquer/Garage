using Garage.Contracts;

namespace Garage.Vehicles.Vehicles;

internal class Bus : Vehicle
{
    public int NumberOfSeats { get; set; }

    public Bus() { }

    public Bus(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
    }

    public Bus(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int numberOfSeats) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        NumberOfSeats = numberOfSeats;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        NumberOfSeats = Utilities.PromptUserForValidNumber("Please enter the number of seats:", ui);
    }

    public override string ToString()
    {
        return base.ToString() + $" Number of seats: {NumberOfSeats}";
    }
}
