using System.Drawing;

namespace Garage.Vehicles;

internal class Airplane : Vehicle
{
    public int NumberOfEngines { get; private set; }

    public Airplane(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int numberOfEngines) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        NumberOfEngines = numberOfEngines;
    }

    public override string ToString()
    {
        return base.ToString() + $" Number of engines: {NumberOfEngines}";
    }
}
