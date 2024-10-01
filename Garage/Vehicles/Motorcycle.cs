using System.Drawing;
using System.Text.Json.Serialization;

namespace Garage.Vehicles;

internal class Motorcycle : Vehicle
{
    public int CylinderVolume { get; private set; }

    public Motorcycle() { }
    
    public Motorcycle(string registration,
    string color,
    int numberOfWheels,
    int maxSpeed,
    string owner,
    int cylinderVolume) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        CylinderVolume = cylinderVolume;
    }

    public override string ToString()
    {
        return base.ToString() + $" Cylinder volume: {CylinderVolume}";
    }
}
