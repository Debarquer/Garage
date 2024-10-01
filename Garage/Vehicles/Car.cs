﻿using System.Text.Json.Serialization;

namespace Garage.Vehicles;

internal class Car : Vehicle
{
    public FuelType FuelType { get; private set; }
    //public string FuelType { get; set; }

    public Car() { }

    public Car(string registration,
    string color,
    int numberOfWheels,
    int maxSpeed,
    string owner,
    FuelType fuelType) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        this.FuelType = fuelType;
    }

    public override string ToString()
    {
        return base.ToString() + $" Fuel type: {FuelType}";
    }
}
