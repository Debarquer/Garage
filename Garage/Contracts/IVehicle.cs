namespace Garage.Contracts;

public interface IVehicle
{
    public string Registration {  get; }
    public string Color { get; }
    public int NumberOfWheels { get; }
    public int MaxSpeed { get; }
    public string Owner { get; }
    public Type MatchableObjectType { get; }
    public IVehicleData Data { get; }

    public void Save(StreamWriter outputFile);
    public void SerializeData(string filePathData);
}
