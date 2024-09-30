using System.Drawing;

namespace Garage.Vehicles;

internal class Boat : Vehicle
{
    public int Length { get; private set; }

    public Boat(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int length) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Length = length;
    }
}
