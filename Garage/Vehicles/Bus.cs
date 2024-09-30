using System.Drawing;

namespace Garage.Vehicles;

internal class Bus : Vehicle
{
    public int NumberOfSeats { get; private set; }

    public Bus(string registration,
    string color,
    int numberOfWheels,
    int maxSpeed,
    string owner,
    int numberOfSeats) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        NumberOfSeats = numberOfSeats;
    }
}
