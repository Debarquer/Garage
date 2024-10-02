using Garage.Contracts;
using System.Collections;

namespace Garage;

internal class Garage<T>: IEnumerable<T> where T : IVehicle
{
    public int Capacity { get; }
    public string Name { get; }

    private GarageSpot<T>[] spots;

    public Garage(int capacity, string name)
    {
        Capacity = capacity;
        spots = new GarageSpot<T>[capacity];
        //for (int i = 0; i < capacity; i++)
        //{
        //    spots[i] = new GarageSpot<T>();
        //}
        Name = name;
    }

    /// <summary>
    /// Adds a vehicle.
    /// </summary>
    /// <param name="vehicle"></param>
    /// <exception cref="Exception">A car with that registration alrady exists in the garage.</exception>
    public void AddVehicle(T vehicle)
    {
        if (HasVehicle(vehicle.Registration))
        {
            throw new Exception($"Failed to add car to garage. A vehicle with the registration {vehicle.Registration} already exists.");
        }

        for(int i = 0; i < Capacity; i++)
        {
            if (spots[i] == null)
            {
                spots[i] = new GarageSpot<T>() { Value = vehicle };
                return;
            }
        }

        throw new Exception("Failed to add car to carage. Garage is full.");
    }

    /// <summary>
    /// Returns all vehicles in the list.
    /// </summary>
    /// <returns></returns>
    public T[] GetAllVehicles()
    {
        return spots
            .Where(x => x != null)
            .Select(x => x.Value)
            .ToArray();
    }

    /// <summary>
    /// Returns all vehicles matching the pattern.
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public T[] GetAllVehicles(Func<T, bool> pattern)
    {
        List<T> result = new List<T>(Capacity);
        for(int i = 0;i < spots.Length; i++) 
        {
            if (spots[i] != null)
            {
                if (pattern(spots[i].Value))
                {
                    result.Add(spots[i].Value);
                }
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Returns a vehicle matching the registration.
    /// </summary>
    /// <param name="registration"></param>
    /// <returns>The vehicle matching the registration.</returns>
    public T GetVehicle(string registration) => spots.Where(x => x != null).Where(x => x!.Value.Registration.ToLower() == registration.ToLower()).FirstOrDefault().Value;

    /// <summary>
    /// Returns whether or not the garage contains a vehicle with the registration.
    /// </summary>
    /// <param name="registration"></param>
    /// <returns>Whether or not the garage contains a vehicle with the registration.</returns>
    public bool HasVehicle(string registration) => spots.Where(x => x != null && x.Value != null).Any(x => x.Value.Registration.ToLower() == registration.ToLower());

    /// <summary>
    /// Removes all matching vehicles.
    /// </summary>
    /// <param name="registration"></param>
    /// <exception cref="ArgumentOutOfRangeException">No vehicles found.</exception>
    public void RemoveVehicle(string registration)
    {
        if (!HasVehicle(registration))
        {
            throw new ArgumentOutOfRangeException($"No vehicles found with registration = {registration}");
        }

        for (int i = 0; i < spots.Length; i++)
        {
            if (spots[i] == null) continue;

            if (spots[i].Value.Registration == registration)
            {
                spots[i] = null;
            }
        }
    }

    /// <summary>
    /// Removes all vehicles from the garage.
    /// </summary>
    public void Clear()
    {
        spots = new GarageSpot<T>[Capacity];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        foreach (GarageSpot<T> spot in spots)
        {
            yield return spot.Value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (GarageSpot<T> spot in spots)
        {
            if(spot != null && spot.Value != null)
                yield return spot.Value;
        }
    }
}
