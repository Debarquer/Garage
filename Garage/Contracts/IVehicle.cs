using System.Drawing;

namespace Garage.Contracts;

internal interface IVehicle
{
    public string Registration {  get; }
    public string Color { get; }
    public int NumberOfWheels { get; }
    public int MaxSpeed { get; }
    public string Owner { get; }

    public void PromptUserForAdditionalData(IUI ui);
    public void Save(StreamWriter outputFile);
    public void SerializeData(string filePathData);
}
