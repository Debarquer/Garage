using Garage.Contracts;

namespace Garage
{
    internal class GarageHandler<T> : IHandler<T> where T : IVehicle
    {
        private Garage<T> garage;
        public int Capacity => garage.Capacity;
        IUI ui;

        public GarageHandler(int capacity, IUI ui)
        {
            garage = new Garage<T>(capacity);
            this.ui = ui;
        }

        public void AddVehicle(T vehicle)
        {
            try
            {
                garage.AddVehicle(vehicle);
            }
            catch(Exception ex)
            {
                ui.PrintMessage($"Garage: Failed to add vehicle with registration {vehicle.Registration}. Error: {ex.Message}");
                return;
            }

            ui.PrintMessage($"Garage: Added vehicle with registration {vehicle.Registration}.");
        }

        public T[] GetAllVehicles()
        {
            return garage.GetAllVehicles();
        }

        public T[] GetAllVehicles(Func<T, bool> pattern)
        {
            T[] result = garage.GetAllVehicles(pattern);
            if(result  == null)
            {
                ui.PrintMessage("No vehicles found.");
                return [];
            }
            else
            {
                return result;
            }
        }

        public T GetVehicle(string registration)
        {
            T vehicle = garage.GetVehicle(registration);
            if(vehicle == null)
            {
                ui.PrintMessage("No vehicle found.");
                return default;
            }

            return vehicle;
        }

        public bool HasVehicle(string registration) => garage.HasVehicle(registration);

        public void RemoveVehicle(string registration)
        {
            try
            {
                garage.RemoveVehicle(registration);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ui.PrintMessage(ex.Message);
            }
        }
    }
}
