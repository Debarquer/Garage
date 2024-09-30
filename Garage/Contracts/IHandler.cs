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
    public T GetVehicle(string registration, string garage);

    /// <summary>
    /// Returns all vehicles in the list.
    /// </summary>
    /// <returns>All vehicles in the list.</returns>
    public T[] GetAllVehicles(string garage);

    /// <summary>
    /// Returns all vehicles in the list that match the pattern.
    /// </summary>
    /// <param name="condition"></param>
    /// <returns>The vehicles in the list that match the pattern.</returns>
    public T[] GetAllVehicles(Func<T, bool> pattern, string garage);

    /// <summary>
    /// Adds a vehicle to the garage.
    /// </summary>
    /// <param name="vehicle"></param>
    public void AddVehicle(T vehicle, string garage);

    /// <summary>
    /// Removes a vehicle from the garage.
    /// </summary>
    /// <param name="registration"></param>
    public void RemoveVehicle(string registration, string garage);

    /// <summary>
    /// Returns true if any vehicles in the garage matches the registration, otherwise returns false.
    /// </summary>
    /// <param name="registration"></param>
    /// <returns>If any vehicles in the garage matches the registration.</returns>
    public bool HasVehicle(string registration, string garage);

    /// <summary>
    /// Prints ToString of all vehicles in the list.
    /// </summary>
    public void PrintAllVehicles(string garage);

    /// <summary>
    /// Prints all the types of vehicles and number of vehicles per type.
    /// </summary>
    /// <param name="garage"></param>
    public void PrintTypes(string garage);
}
