using Garage;
using Garage.Contracts;
using Garage.Vehicles.Vehicles;

namespace GarageTests
{
    public class GarageHandlerTests
    {
        [Fact]
        public void AddVehicle_VehicleAdded_Success()
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
    }
}