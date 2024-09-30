using Garage.Contracts;
using Garage.Vehicles;
using System.Numerics;

namespace Garage
{
    internal class GarageHandler<T> : IHandler<T> where T : IVehicle
    {
        //private Garage<T> garage;
        private Dictionary<string, Garage<T>> garages = [];
        //public int Capacity => garage.Capacity;
        IUI ui;

        public GarageHandler(int capacity, IUI ui)
        {
            garages["default"] = new Garage<T>(capacity,"default");

            this.ui = ui;

            IVehicle car1 = new Car("abc 123", "red", 4, 100, "simon", FuelType.gas);
            AddVehicle((T)car1, "default");
            IVehicle car2 = new Car("abcd 123", "blue", 4, 100, "simon", FuelType.gas);
            AddVehicle((T)car2, "default");
            IVehicle car3 = new Car("abcd 1234", "red", 4, 120, "daniel", FuelType.gas);
            AddVehicle((T)car3, "default");
            IVehicle car4 = new Car("abc 1234", "blue", 4, 120, "daniel", FuelType.diesel);
            AddVehicle((T)car4, "default");
            IVehicle bus1 = new Bus("DEF 1234", "orange", 4, 80, "anna", 20);
            AddVehicle((T)bus1, "default");
            IVehicle bus2 = new Bus("DGH 1234", "purple", 4, 90, "mo",35);
            AddVehicle((T)bus2, "default");
            IVehicle airplane1 = new Airplane("IHJ 1234", "white", 4, 600, "peter", 6);
            AddVehicle((T)airplane1, "default");
        }

        public void AddVehicle(T vehicle, string garage)
        {
            garage = garage.ToLower();

            try
            {
                garages[garage].AddVehicle(vehicle);
            }
            catch (KeyNotFoundException ex)
            {
                ui.PrintMessage($"Garage: Failed to add vehicle with registration {vehicle.Registration}. No garage with name {garage} found.");
                return;
            }
            catch (Exception ex)
            {
                ui.PrintMessage($"Garage: Failed to add vehicle with registration {vehicle.Registration}.");
                return;
            }

            ui.PrintMessage($"Garage: Added vehicle with registration {vehicle.Registration}.");
        }

        public T[] GetAllVehicles(string garage)
        {
            garage = garage.ToLower();

            return garages[garage].GetAllVehicles();
        }

        public T[] GetAllVehicles(Func<T, bool> pattern, string garage)
        {
            garage = garage.ToLower();

            if (!garages.ContainsKey(garage))
            {
                ui.PrintMessage($"Garage: No garage with name {garage} found.");
                return default;
            }

            T[] result = garages[garage].GetAllVehicles(pattern);
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

        public T GetVehicle(string registration, string garage)
        {
            registration = registration.ToLower();
            garage = garage.ToLower();

            if (!garages.ContainsKey(garage))
            {
                ui.PrintMessage($"Garage: No garage with name {garage} found.");
                return default;
            }

            T vehicle = garages[garage].GetVehicle(registration);
            if(vehicle == null)
            {
                ui.PrintMessage("No vehicle found.");
                return default;
            }

            return vehicle;
        }

        public bool HasVehicle(string registration, string garage) => garages[garage.ToLower()].HasVehicle(registration.ToLower());

        public void RemoveVehicle(string registration, string garage)
        {
            registration = registration.ToLower();
            garage = garage.ToLower();

            try
            {
                garages[garage].RemoveVehicle(registration);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ui.PrintMessage(ex.Message);
                return;
            }
            catch(KeyNotFoundException ex)
            {
                ui.PrintMessage(ex.Message);
                return;
            }

            ui.PrintMessage($"Garage: Removed vehicle with registration {registration}.");
        }

        public void PrintAllVehicles(string garage)
        {
            garage = garage.ToLower();
            if (!garages.ContainsKey(garage))
            {
                ui.PrintMessage($"Garage: No garage with name {garage} found.");
                return;
            }

            T[] vehicles = garages[garage].GetAllVehicles();
            ui.PrintMessage($"Vehicles in garage {garages[garage].Name}: ");
            foreach(T vehicle in vehicles)
            {
                ui.PrintMessage($"{vehicle.GetType().Name}: {vehicle}");
            }
        }

        public void PrintTypes(string garage)
        {
            T[] vehicles = GetAllVehicles(garage.ToLower());
            IEnumerable<IGrouping<Type, T>> grouping = vehicles.GroupBy(x => x.GetType());

            if(grouping.Count() == 0) 
            {
                ui.PrintMessage("No vehicles found.");
                return;
            }

            foreach (IGrouping<Type, T> item in grouping)
            {
                ui.PrintMessage($"{item.Key.Name}: {item.Count()}");
            }
        }

        public void AddGarage(string garage, int capacity)
        {
            if (garages.ContainsKey(garage))
            {
                ui.PrintMessage($"Garage {garage} already exists!");
                return;
            }

            garages[garage] = new Garage<T>(capacity, garage);

            ui.PrintMessage($"Added garage {garage} with capacity {capacity}");
        }

        public void PrintGarages()
        {
            foreach(Garage<T> garage in garages.Values)
            {
                ui.PrintMessage($"{garage.Name} Occupancy: {garage.GetAllVehicles().Length}/{garage.Capacity}");
            }
        }

        public void PrintVehiclesMatchingPattern(string garage,
                                                 string? registration,
                                                 string? color,
                                                 int? numberOfWheels,
                                                 int? maxSpeed,
                                                 string? owner,
                                                 string? type)
        {
            Func<T, bool> pattern = (T vehicle) => (
                    (registration == null || registration == vehicle.Registration) &&
                    (color == null || color == vehicle.Color) &&
                    (numberOfWheels == null || numberOfWheels == vehicle.NumberOfWheels) &&
                    (maxSpeed == null || maxSpeed == vehicle.MaxSpeed) &&
                    (owner == null || owner == vehicle.Owner)) &&
                    (type == null || type.ToLower() == vehicle.GetType().Name.ToLower());

            T[] vehicles = GetAllVehicles(pattern, garage);

            if(vehicles == null || vehicles.Length == 0)
            {
                ui.PrintMessage($"No vehicles found in garage {garage}");
                return;
            }

            foreach(T vehicle in vehicles)
            {
                ui.PrintMessage(vehicle.ToString());
            }
        }
    }
}
