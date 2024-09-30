namespace Garage.Contracts;

internal interface IHandler<T> where T : IVehicle
{
    public int Capacity { get; }

    public T GetVehicle(int registration);
    public T[] GetAllVehicles();
    public T[] GetAllVehicles(Func<T, bool> condition);
    public void AddVehicle(T registration);
    public void RemoveVehicle(T registration);
    public bool HasVehicle(T registration);
}
