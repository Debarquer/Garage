using Garage;
using Garage.Contracts;
using Garage.Vehicles;
using Garage.Vehicles.Vehicles;
using NuGet.Frameworks;

namespace GarageTests
{
    public class GarageHandlerTests
    {
        [Fact]
        public void AddVehicle_HasVehicle_Success()
        {
            GarageHandler<IVehicle> garageHandler = GarageHandlerFactory.CreateGarageHandler();
            garageHandler.AddGarage("default", 1);
            IVehicle vehicle = new Car("ABC 123", "red", 4, 120, "John Doe");

            if(garageHandler.HasVehicle(vehicle.Registration, "default"))
            {
                throw new Exception("Vehicle with that registration already exists");
            }

            garageHandler.AddVehicle(vehicle, "default");

            Assert.True(garageHandler.HasVehicle(vehicle.Registration, "default"));
        }

        //todo: rewrite test to not use the HasVehicle method to test itself
        [Fact]
        public void HasVehicle_ContainsVehicle_True()
        {
            GarageHandler<IVehicle> garageHandler = GarageHandlerFactory.CreateGarageHandler();
            garageHandler.AddGarage("default", 1);
            IVehicle vehicle = new Car("ABC 123", "red", 4, 120, "John Doe");

            if (garageHandler.HasVehicle(vehicle.Registration, "default"))
            {
                throw new Exception("Vehicle with that registration already exists");
            }

            garageHandler.AddVehicle(vehicle, "default");

            Assert.True(garageHandler.HasVehicle(vehicle.Registration, "default"));
        }

        [Fact]
        public void GetVehicle_ReturnValue_IsSameVehicle()
        {
            GarageHandler<IVehicle> garageHandler = GarageHandlerFactory.CreateGarageHandler();
            garageHandler.AddGarage("default", 1);
            IVehicle vehicle = new Car("ABC 123", "red", 4, 120, "John Doe");

            if (garageHandler.HasVehicle(vehicle.Registration, "default"))
            {
                throw new Exception("Vehicle with that registration already exists");
            }

            garageHandler.AddVehicle(vehicle, "default");

            IVehicle other = garageHandler.GetVehicle(vehicle.Registration, "default");

            Assert.Equal(vehicle, other);
        }

        [Fact]
        public void RemoveVehicle_VehicleCount_OneLower()
        {
            GarageHandler<IVehicle> garageHandler = GarageHandlerFactory.CreateGarageHandler();
            garageHandler.AddGarage("default", 3);

            garageHandler.AddVehicle(new Car("ABC 123", "red", 4, 120, "John Doe"), "default");
            garageHandler.AddVehicle(new Car("ABC 1234", "red", 4, 120, "John Doe"), "default");
            garageHandler.AddVehicle(new Car("ABC 12345", "red", 4, 120, "John Doe"), "default");

            int expected = garageHandler.GetGarage("default").GetAllVehicles().Length - 1;

            garageHandler.RemoveVehicle("ABC 12345", "default");

            Assert.Equal(expected, garageHandler.GetGarage("default").GetAllVehicles().Length);
        }

        //[Fact]
        //public void GetVehicles_ReturnValues_AreSameVehicles()
        //{
        //    GarageHandler<IVehicle> garageHandler = GarageHandlerFactory.CreateGarageHandler();
        //    garageHandler.AddGarage("default", 1);
        //    IVehicle[] vehicles = {
        //        new Car("ABC 123", "red", 4, 120, "John Doe"),
        //        new Car("ABC 1234", "red", 4, 120, "John Doe"),
        //        new Car("ABC 1235", "red", 4, 120, "John Doe")
        //    };

        //    foreach(IVehicle vehicle in vehicles)
        //    {
        //        if (garageHandler.HasVehicle(vehicle.Registration, "default"))
        //        {
        //            throw new Exception("Vehicle with that registration already exists");
        //        }

        //        garageHandler.AddVehicle(vehicle, "default");
        //    }

        //    IVehicle[] others = garageHandler.GetAllVehicles("default");

        //    for (int i = 0; i < vehicles.Length; i++)
        //    {
        //        Assert.Equal(vehicles[i], others[i]);
        //    }
        //}

        [Fact]
        public void AddGarage_HasGarage_True()
        {
            GarageHandler<IVehicle> garageHandler = GarageHandlerFactory.CreateGarageHandler();

            if (garageHandler.HasGarage("default"))
            {
                throw new Exception("Garage with that name already exists");
            }

            garageHandler.AddGarage("default", 1);

            Assert.True(garageHandler.HasGarage("default"));
        }

        [Fact]
        public void ClearGarage_GetVehiclesCount_0()
        {
            GarageHandler<IVehicle> garageHandler = GarageHandlerFactory.CreateGarageHandler();
            garageHandler.AddGarage("default", 1);

            Garage<IVehicle> garage = garageHandler.GetGarage("default");
            garageHandler.AddVehicle(new Car("ABC 123", "red", 4, 120, "John Doe"), "default");
            garageHandler.AddVehicle(new Car("ABC 1234", "red", 4, 120, "John Doe"), "default");
            garageHandler.AddVehicle(new Car("ABC 1235", "red", 4, 120, "John Doe"), "default");

            if (garage.GetAllVehicles().Length == 0)
            {
                throw new Exception("Failed to add vehicles.");
            }

            garageHandler.ClearGarage("default");

            Assert.Equal(0, garage.GetAllVehicles().Length);
        }

        [Fact]
        public void GetNumberOfGarages_ReturnValue_1()
        {
            GarageHandler<IVehicle> garageHandler = GarageHandlerFactory.CreateGarageHandler();

            if (garageHandler.GetNumberOfGarages() != 0)
            {
                throw new Exception("Incorrect number of garages.");
            }

            garageHandler.AddGarage("default1", 1);

            Assert.Equal(1, garageHandler.GetNumberOfGarages());
        }

        [Fact]
        public void RemoveGarage_ReturnValue_1()
        {
            GarageHandler<IVehicle> garageHandler = GarageHandlerFactory.CreateGarageHandler();
            garageHandler.AddGarage("default1", 1);
            garageHandler.AddGarage("default2", 2);

            if (garageHandler.GetNumberOfGarages() != 2)
            {
                throw new Exception("Incorrect number of garages.");
            }

            garageHandler.RemoveGarage("default2");

            Assert.Equal(1, garageHandler.GetNumberOfGarages());
        }
    }
}