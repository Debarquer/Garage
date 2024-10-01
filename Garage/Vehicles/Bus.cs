using System.Drawing;
using System.Text.Json.Serialization;

namespace Garage.Vehicles;

internal class Bus : Vehicle
{
    public int NumberOfSeats { get; private set; }

    public Bus() { }

    public Bus(string registration,
    string color,
    int numberOfWheels,
    int maxSpeed,
    string owner,
    int numberOfSeats) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        NumberOfSeats = numberOfSeats;
    }

    public override string ToString()
    {
        return base.ToString() + $" Number of seats: {NumberOfSeats}";
    }
}
