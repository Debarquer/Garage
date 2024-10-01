using System.Drawing;
using System.Text.Json.Serialization;

namespace Garage.Vehicles;

internal class Boat : Vehicle
{
    public int Length { get; private set; }

    public Boat() { }

    public Boat(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int length) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Length = length;
    }

    public override string ToString()
    {
        return base.ToString() + $" Length: {Length}";
    }
}
