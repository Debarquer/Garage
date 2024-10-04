using Garage.Contracts;
using Garage.Vehicles;
using Garage.Vehicles.Vehicles;
using Newtonsoft.Json;
using System.Reflection;

namespace Garage;

public class GarageHandler<T> : IHandler<T> where T : IVehicle
{
    private Dictionary<string, Garage<T>> garages = [];
    IUI ui;

    public GarageHandler(IUI ui)
    {
        this.ui = ui;

        //SeedData();
    }

    private void SeedData()
    {
        garages["default"] = new Garage<T>(100, "default");
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
        IVehicle bus2 = new Bus("DGH 1234", "purple", 4, 90, "mo", 35);
        AddVehicle((T)bus2, "default");
        IVehicle airplane1 = new Airplane("IHJ 1234", "white", 4, 600, "peter", 6);
        AddVehicle((T)airplane1, "default");
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

    public void PrintAllVehicles()
    {
        if(garages.Count == 0)
        {
            ui.PrintMessage("No garages available.");
            return;
        }

        foreach(Garage<T> garage in garages.Values)
        {
            PrintAllVehicles(garage.Name);
        }
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

    public void RemoveGarage(string name)
    {
        if (!HasGarage(name))
        {
            ui.PrintMessage($"Failed to remove garage. No garage found with name: {name}");
            return;
        }

        ClearGarage(name);
        garages.Remove(name);
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

    public void ClearGarage(string garageName)
    {
        if (!ValidateGarage(garageName)) return;

        Garage<T> garage = GetGarage(garageName);

        garage.Clear();
        ui.PrintMessage($"Garage {garageName} cleared");
    }

    public int GetNumberOfGarages() => garages.Count;

    public void PrintAllVehiclesMatchingPattern(string[] parameters)
    {
        if(garages.Count == 0)
        {
            ui.PrintMessage("No garages available.");
            return;
        }

        foreach(Garage<T> garage in garages.Values)
        {
            PrintVehiclesMatchingPattern(garage.Name, parameters);
        }
    }

    public void PrintVehiclesMatchingPattern(string garageName, string[] parameters)
    {
        if (!ValidateGarage(garageName)) return;

        Garage<T> garage = GetGarage(garageName);
        IPatternMatchable[] allObjects = GetAllVehicles(garage).Select(x => x as IPatternMatchable).ToArray();

        if (allObjects == null || allObjects.Length == 0)
        {
            ui.PrintMessage($"No vehicles found in garage {garage}");
            return;
        }

        PatternMatcher<IPatternMatchable> patternMatcher = new();
        IPatternMatchable[] matchingVehicles = patternMatcher.GetObjectsMatchingPattern(allObjects, parameters);

        ui.PrintMessage($"{matchingVehicles.Length} matches in {garageName}.");
        foreach(IPatternMatchable vehicle in matchingVehicles)
        {
            ui.PrintMessage($"{vehicle}");
        }
    }

    public void SaveAll()
    {
        if(garages.Count == 0)
        {
            ui.PrintMessage("No garages to save");
            return;
        }

        SaveAll(Directories.SavePath);
    }

    public void SaveAll(string directory)
    {
        try
        {
            foreach (Garage<T> garage in garages.Values)
            {
                Save(garage, directory);
            }
        }
        catch(Exception ex)
        {
            ui.PrintMessage(ex.Message);
        }
    }

    public void Save(string garageName)
    {
        if (!ValidateGarage(garageName)) return;

        Garage<T> garage = GetGarage(garageName);
        Save(garage, Directories.SavePath);
    }

    public void Save(Garage<T> garage, string directory)
    {
        string filePath = Path.Combine(directory, garage.Name + ".txt");

        if (!File.Exists(filePath))
        {
            var v = File.Create(filePath);
            v.Close();
        }

        using StreamWriter outputFile = new StreamWriter(filePath, false);
        outputFile.WriteLine(garage.Capacity.ToString());
        foreach (T vehicle in garage)
        {
            vehicle.Save(outputFile);
        }

        ui.PrintMessage($"Saved {garage.Name} to {filePath}");
    }

    public void LoadAll()
    {
        LoadAll(Directories.SavePath);
    }

    public void LoadAll(string directory)
    {
        try
        {
            foreach(string f in Directory.GetFiles(directory))
            {
                Load(f);
            }
        }
        catch (Exception ex)
        {
            ui.PrintMessage(ex.Message);
        }
    }

    public void Load(string filePath)
    {
        try
        {
            string dataPath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + "data" + Path.GetExtension(filePath));
            using (StreamReader inputStreamReader = new StreamReader(filePath))
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
                    vehicle.SerializeData(line);
                    garage.AddVehicle(vehicle);
                    ui.PrintMessage($"Loaded {vehicle.Registration}");
                }
            }
        }
        catch(Exception ex)
        {
            ui.PrintMessage(ex.Message);
        }
    }
}
