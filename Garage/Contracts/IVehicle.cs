namespace Garage.Contracts;

public interface IVehicle
{
    public string Registration {  get; }
    public string Color { get; }
    public int NumberOfWheels { get; }
    public int MaxSpeed { get; }
    public string Owner { get; }
    public Type DataType { get; }
    public IVehicleData Data { get; }

    public void PromptUserForAdditionalData(IUI ui);
    public void Save(StreamWriter outputFile);
    public void SerializeData(string filePathData);
}
