using Garage.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal abstract class Vehicle : IVehicle
    {
        public string Registration { get; protected set; }

        public Color Color => throw new NotImplementedException();

        public int NumberOfWheels => throw new NotImplementedException();

        public int MaxSpeed => throw new NotImplementedException();

        public string Owner => throw new NotImplementedException();

        public Vehicle(string registration)
        {
            Registration = registration;
        }
    }
}
