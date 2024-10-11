namespace Garage.Contracts;

public interface IHandler<T> where T : IVehicle
{
    /// <summary>
    /// The capacity of the garage. Readonly.
    /// </summary>
    //public int Capacity { get; }

    /// <summary>
    /// Returns a vehicle matching the registration.
    /// </summary>
    /// <param name="registration"></param>
    /// <returns>The vehicle matching the registration.</returns>
    public T GetVehicle(string registration, string garageName);

    /// <summary>
    /// Adds a vehicle to the garage.
    /// </summary>
    /// <param name="vehicle"></param>
    public void AddVehicle(T vehicle, string garageName);

    /// <summary>
    /// Removes a vehicle from the garage.
    /// </summary>
    /// <param name="registration"></param>
    public void RemoveVehicle(string registration, string garageName);

    /// <summary>
    /// Returns true if any vehicles in the garage matches the registration, otherwise returns false.
    /// </summary>
    /// <param name="registration"></param>
    /// <returns>If any vehicles in the garage matches the registration.</returns>
    public bool HasVehicle(string registration, string garageName);

    /// <summary>
    /// Prints ToString of all vehicles in the list.
    /// </summary>
    public void PrintAllVehicles(string garageName);

    /// <summary>
    /// Calls PrintTypes on all stored garages.
    /// </summary>
    public void PrintAllTypes();

    /// <summary>
    /// Prints all the types of vehicles and number of vehicles per type.
    /// </summary>
    /// <param name="garage"></param>
    public void PrintTypes(string garageName);

    /// <summary>
    /// Attemps to create a new garage and store it in the handler.
    /// If a garage with that name alrady exists then it instead returns that garage.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="capacity"></param>
    /// <returns>The garage with the specified name.</returns>
    public Garage<T> AddGarage(string name, int capacity);

    /// <summary>
    /// Removes a garage with the specified name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public void RemoveGarage(string name);

    /// <summary>
    /// Returns the garage with the matching name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Garage<T> GetGarage(string name);

    /// <summary>
    /// Returns whether or not a garage exists with that name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Whether or not a garage exists with that name.</returns>
    public bool HasGarage(string name);

    /// <summary>
    /// Returns the number of garages.
    /// </summary>
    /// <returns></returns>
    public int GetNumberOfGarages();

    /// <summary>
    /// Prints all contained garages to the ui.
    /// </summary>
    public void PrintGarages();

    /// <summary>
    /// Calls PrintVehiclesMatchingPattern on all stored garages.
    /// </summary>
    /// <param name="parameters"></param>
    public void PrintAllVehiclesMatchingPattern(string[] parameters);

    /// <summary>
    /// Prints all the vehicles from the garage matching the pattern.
    /// </summary>
    /// <param name="garage"></param>
    /// <param name="parameters"></param>
    public void PrintVehiclesMatchingPattern(string garageName, string[] parameters);

    /// <summary>
    /// Saves all garages to files.  Uses the directory specified in the Directories utility class.
    /// </summary>
    public void SaveAll();

    /// <summary>
    /// Saves all garages to files created in the given directory.
    /// </summary>
    /// <param name="path"></param>
    public void SaveAll(string directory);

    /// <summary>
    /// Find a garage matching the name and save it to a file.  Uses the directory specified in the Directories utility class.
    /// </summary>
    /// <param name="garageName"></param>
    public void Save(string garageName);

    /// <summary>
    /// Save the specified garage to a file in the specified directory.
    /// </summary>
    /// <param name="garage"></param>
    public void Save(Garage<T> garage, string directory);

    /// <summary>
    /// Load in all garges from files. Uses the directory specified in the Directories utility class.
    /// </summary>
    public void LoadAll();

    /// <summary>
    /// Load in all garages from files found in the specified directory.
    /// </summary>
    /// <param name="directory"></param>
    public void LoadAll(string directory);

    /// <summary>
    /// Loads a garage from the specified path.
    /// </summary>
    /// <param name="filePath"></param>
    public void Load(string filePath);
}
