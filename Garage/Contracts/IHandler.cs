namespace Garage.Contracts;

internal interface IHandler<T> where T : IVehicle
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
    /// Prints all contained garages to the ui.
    /// </summary>
    public void PrintGarages();

    /// <summary>
    /// Prints all the vehicles from the garage matching the pattern.
    /// </summary>
    /// <param name="garage"></param>
    /// <param name="parameters"></param>
    public void PrintVehiclesMatchingPattern(string garageName, string[] parameters);
}
