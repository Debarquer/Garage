using Garage.Contracts;
using Garage.Vehicles;
using Newtonsoft.Json;

namespace Garage;

internal class GarageHandler<T> : IHandler<T> where T : IVehicle
{
    //private Garage<T> garage;
    private Dictionary<string, Garage<T>> garages = [];
    //public int Capacity => garage.Capacity;
    IUI ui;

    string savePath = "data\\garages";

    public GarageHandler(int capacity, IUI ui)
    {
        garages["default"] = new Garage<T>(capacity,"default");

        this.ui = ui;

        //IVehicle car1 = new Car("abc 123", "red", 4, 100, "simon", FuelType.gas);
        //AddVehicle((T)car1, "default");
        //IVehicle car2 = new Car("abcd 123", "blue", 4, 100, "simon", FuelType.gas);
        //AddVehicle((T)car2, "default");
        //IVehicle car3 = new Car("abcd 1234", "red", 4, 120, "daniel", FuelType.gas);
        //AddVehicle((T)car3, "default");
        //IVehicle car4 = new Car("abc 1234", "blue", 4, 120, "daniel", FuelType.diesel);
        //AddVehicle((T)car4, "default");
        //IVehicle bus1 = new Bus("DEF 1234", "orange", 4, 80, "anna", 20);
        //AddVehicle((T)bus1, "default");
        //IVehicle bus2 = new Bus("DGH 1234", "purple", 4, 90, "mo", 35);
        //AddVehicle((T)bus2, "default");
        //IVehicle airplane1 = new Airplane("IHJ 1234", "white", 4, 600, "peter", 6);
        //AddVehicle((T)airplane1, "default");
    }

    public void AddVehicle(T vehicle, string garageName)
    {
        garageName = garageName.ToLower();

        Garage<T> garage = GetGarage(garageName);
        try
        {
            garage.AddVehicle(vehicle);
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

    private T[] GetAllVehicles(Garage<T> garage) => garage.GetAllVehicles();

    /// <summary>
    /// Returns a vehicle matching the registration.
    /// </summary>
    /// <param name="registration"></param>
    /// <param name="garageName"></param>
    /// <returns>The vehicle matching the registration.</returns>
    /// <exception cref="Exception">If no garage is found with that name.</exception>
    public T GetVehicle(string registration, string garageName)
    {
        Garage<T> garage;
        try
        {
            garage = GetGarage(garageName);

        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }

        registration = registration.ToLower();

        T vehicle = garage.GetVehicle(registration);
        if (vehicle == null)
        {
            ui.PrintMessage("No vehicle found.");
            return default;
        }

        return vehicle;
    }

    /// <summary>
    /// Returns true if any vehicles in the garage matches the registration, otherwise returns false.
    /// </summary>
    /// <param name="registration"></param>
    /// <param name="garageName"></param>
    /// <returns>If any vehicles in the garage matches the registration.</returns>
    /// <exception cref="Exception">If no garage is found with that name.</exception>
    public bool HasVehicle(string registration, string garageName)
    {
        Garage<T> garage;
        try
        {
            garage = GetGarage(garageName);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return garage.HasVehicle(registration.ToLower());
    }

    public void RemoveVehicle(string registration, string garageName)
    {
        if (!ValidateGarage(garageName)) return;

        Garage<T> garage = GetGarage(garageName);
        registration = registration.ToLower();

        try
        {
            garage.RemoveVehicle(registration);
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

    public void PrintAllVehicles(string garageName)
    {
        if (!ValidateGarage(garageName)) return;

        Garage<T> garage = GetGarage(garageName);

        ui.PrintMessage($"Vehicles in garage {garage.Name}: ");
        foreach (T vehicle in garage)
        {
            ui.PrintMessage($"{vehicle.GetType().Name}: {vehicle}");
        }
    }

    public void PrintTypes(string garageName)
    {
        if (!ValidateGarage(garageName)) return;

        Garage<T> garage = GetGarage(garageName);

        T[] vehicles = GetAllVehicles(garage);
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

    public Garage<T> AddGarage(string name, int capacity)
    {
        if (garages.ContainsKey(name))
        {
            ui.PrintMessage($"Garage {name} already exists!");
            return garages[name];
        }

        garages[name] = new Garage<T>(capacity, name);

        ui.PrintMessage($"Added garage {name} with capacity {capacity}");
        return garages[name];
    }

    public Garage<T> GetGarage(string garage)
    {
        garage = garage.ToLower();
        if (!garages.ContainsKey(garage))
        {
            throw new IndexOutOfRangeException($"Garage: No garage with name {garage} found.");
        }

        return garages[garage];
    }

    private Garage<T> AddOrGet(string name, int capacity)
    {
        if(HasGarage(name)) return GetGarage(name);
        else return AddGarage(name, capacity);
    }

    private bool ValidateGarage(string name)
    {
        name = name.ToLower();
        if (!HasGarage(name))
        {
            ui.PrintMessage($"Garage: No garage with name {name} found.");
            return false;
        }

        return true;
    }

    public bool HasGarage(string name)
    {
        return garages.ContainsKey(name);
    }

    public void PrintGarages()
    {
        foreach(Garage<T> garage in garages.Values)
        {
            ui.PrintMessage($"{garage.Name} Occupancy: {garage.GetAllVehicles().Length}/{garage.Capacity}");
        }
    }

    public void PrintVehiclesMatchingPattern(string garageName, string[] parameters)
    {
        if (!ValidateGarage(garageName)) return;

        Garage<T> garage = GetGarage(garageName);
        T[] v = GetAllVehicles(garage);

        if(v == null || v.Length == 0)
        {
            ui.PrintMessage($"No vehicles found in garage {garage}");
            return;
        }

        bool matches = true;

        List<T> vehicles = new List<T>();   

        foreach(var vehicle in v)
        {
            matches = true;
            foreach(string s in parameters)
            {
                string[] parameterSplit = s.Split(':');
                if (parameterSplit.Length != 2)
                {
                    ui.PrintMessage("Invalid paramter");
                    matches = false;
                    break;
                }

                string parameterName = parameterSplit[0];
                string parameterValue = parameterSplit[1];

                if(parameterName == "type")
                {
                    if(parameterValue.ToLower() == vehicle.GetType().Name.ToLower())
                    {
                        continue;
                    }
                    else
                    {
                        matches = false;
                        break;
                    }
                }

                var props = vehicle.GetType().GetProperties();

                bool foundProp = false;
                foreach (var prop in props)
                {
                    if (prop.Name.ToLower() == parameterName.ToLower())
                    {
                        foundProp = true;
                        if (prop.GetValue(vehicle).ToString() != parameterValue)
                        {
                            matches = false;
                            break;
                        }
                    }
                }
                if (!foundProp)
                {
                    matches = false;
                    break;
                }
            }

            if(matches)
            {
                vehicles.Add(vehicle);
            }
        }

        ui.PrintMessage($"{vehicles.Count} matches.");
        foreach(var vehicle in vehicles)
        {
            ui.PrintMessage($"{vehicle.ToString()}");
        }
    }

    public void SaveAll()
    {
        SaveAll(savePath);
    }

    public void SaveAll(string path)
    {
        try
        {
            foreach (Garage<T> garage in garages.Values)
            {
                Save(garage);
            }
        }
        catch(Exception ex)
        {
            ui.PrintMessage(ex.Message);
        }
    }

    private void Save(Garage<T> garage)
    {
        string filePath = Path.Combine(savePath, garage.Name + ".txt");
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }

        using (StreamWriter outputFile = new StreamWriter(filePath))
        {
            outputFile.WriteLine(garage.Capacity.ToString());
            foreach(T v in garage)
            {
                SerializeJson(outputFile, v);
                //SerializeReflection(outputFile, v);
            }
        }

        ui.PrintMessage($"Saved {garage.Name} to {filePath}");
    }

    private static void SerializeJson(StreamWriter outputFile, T v)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        string s = JsonConvert.SerializeObject(v, settings);
        outputFile.WriteLine(s);
    }

    private static void SerializeReflection(StreamWriter outputFile, IVehicle v)
    {
        string s = "";
        s += $"type:{v.GetType().Name};";
        foreach(var prop in v.GetType().GetProperties())
        {
            s += $"{prop.Name}:{prop.GetValue(v)};";
        }
        outputFile.WriteLine(s);
    }

    public void LoadAll()
    {
        LoadAll(savePath);
    }

    public void LoadAll(string path)
    {
        try
        {
            foreach(string f in Directory.GetFiles(savePath))
            {
                Load(f);
            }
        }
        catch (Exception ex)
        {
            ui.PrintMessage(ex.Message);
        }
    }

    private void Load(string filePath)
    {
        using(StreamReader inputStreamReader = new StreamReader(filePath))
        {
            string line = inputStreamReader.ReadLine();
            if (!int.TryParse(line, out int capacity))
            {
                ui.PrintMessage($"Failed to load {filePath}: invalid capacity");
                return;
            }

            Garage<IVehicle> garage = AddOrGet(Path.GetFileNameWithoutExtension(filePath), capacity) as Garage<IVehicle>;
            while (!inputStreamReader.EndOfStream)
            {
                line = inputStreamReader.ReadLine();
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                IVehicle vehicle = JsonConvert.DeserializeObject<IVehicle>(line, settings);
                //IVehicle v = DeserializeReflection(line);
                garage.AddVehicle(vehicle);
                ui.PrintMessage($"Loaded {vehicle.Registration}");
            }
        }
    }

    private IVehicle DeserializeReflection(string s)
    {
        string[] splitA = s.Split(';');

        string type = splitA[0].Split(':')[1];
        IVehicle v = null;

        switch (type)
        {
            case "Airplane":
                v = new Airplane();
                break;
            case "Boat":
                v = new Boat();
                break;
            case "Bus":
                v = new Bus();
                break;
            case "Car":
                v = new Car();
                break;
            case "Motorcycle":
                v = new Motorcycle();
                break;
        }

        for (int i = 1; i < splitA.Length; i++)
        {
            string f = splitA[i];
            string[] splitB = f.Split(":");
            if(splitB.Length < 2)
            {
                ui.PrintMessage("Outside bounds");
                continue;
            }
            string name = splitB[0];
            string value = splitB[1];

            foreach (var prop in v.GetType().GetProperties())
            {
                if (prop.Name == name)
                {
                    if(prop.PropertyType.Name == "Int32")
                    {
                        prop.SetValue(v, int.Parse(value), null);
                    }
                    else
                    {
                        prop.SetValue(v, value as System.String, null);
                    }
                }
            }
        }

        return v;
    }
}
