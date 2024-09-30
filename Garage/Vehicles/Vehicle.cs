using Garage.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Vehicles
{
    internal abstract class Vehicle : IVehicle
    {
        public string Registration { get; protected set; }

        public string Color { get; protected set; }

        public int NumberOfWheels { get; protected set; }

        public int MaxSpeed { get; protected set; }

        public string Owner { get; protected set; }

        public Vehicle(
            string registration,
            string color,
            int numberOfWheels,
            int maxSpeed,
            string owner) 
        {  
            Registration = registration;
            Color = color;
            NumberOfWheels = numberOfWheels;
            MaxSpeed = maxSpeed;
            Owner = owner;
        }

        public override string ToString()
        {
            return $"{Registration}: {Color} {NumberOfWheels} wheels Max {MaxSpeed}kmph Owner: {Owner}";
        }
    }
}
