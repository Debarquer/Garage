using System.Drawing;

namespace Garage.Vehicles;

internal class Car : Vehicle
{
    FuelType FuelType;

    public Car(string registration,
    string color,
    int numberOfWheels,
    int maxSpeed,
    string owner,
    FuelType fuelType) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        this.FuelType = fuelType;
    }
}
