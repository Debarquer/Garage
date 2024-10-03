namespace Garage.Contracts;

public interface IVehicleData
{
    public string Registration { get; set; }
    public string Color { get; set; }
    public int NumberOfWheels { get; set; }
    public int MaxSpeed { get; set; }
    public string Owner { get; set; }
}
