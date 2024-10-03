using Garage.Contracts;
using Newtonsoft.Json;

namespace Garage.Vehicles.Vehicles;

internal class Motorcycle : Vehicle
{
    public class MotorcycleData : VehicleData
    {
        public int CylinderVolume { get; set; }
    }

    public int CylinderVolume
    {
        get => ((MotorcycleData)Data).CylinderVolume;
        private set => ((MotorcycleData)Data).CylinderVolume = value;
    }
    public Motorcycle() 
    { 
        Data = new MotorcycleData();
    }

    public Motorcycle(string registration,
    string color,
    int numberOfWheels,
    int maxSpeed,
    string owner) : base()
    {
        Data = new MotorcycleData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
    }

    public Motorcycle(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int cylinderVolume) : base()
    {
        Data = new MotorcycleData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
        CylinderVolume = cylinderVolume;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        CylinderVolume = Utilities.PromptUserForValidNumber("Please enter the cylinder volume:", ui);
    }

    public override string ToString()
    {
        return base.ToString() + $" Cylinder volume: {CylinderVolume}";
    }

    public override void SerializeData(string line)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        this.Data = JsonConvert.DeserializeObject<MotorcycleData>(line, settings);
    }
}
