using System.Drawing;

namespace Garage.Vehicles;

internal class Motorcycle : Vehicle
{
    public int CylinderVolume { get; private set; }

    public Motorcycle(string registration,
    string color,
    int numberOfWheels,
    int maxSpeed,
    string owner,
    int cylinderVolume) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        CylinderVolume = cylinderVolume;
    }
}
