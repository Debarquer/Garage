using Garage.Contracts;
using Newtonsoft.Json;
using static Garage.Vehicles.Vehicles.Bus;

namespace Garage.Vehicles.Vehicles;

internal class Motorcycle : Vehicle
{
    public class MotorcycleData : VehicleData
    {
        public int CylinderVolume { get; set; }

        public static VehicleData GetData(IUI ui, IHandler<IVehicle> garageHandler, string garageName)
        {
            MotorcycleData data = new MotorcycleData();

            data.Registration = Utilities.PromptUserForValidInput(
                "Please enter the registration:",
                (string s) =>
                {
                    return s.Length > 0 && !garageHandler.HasVehicle(s, garageName);
                },
                ui,
                "Vehicle with that registration already exists or the input is empty."
            );

            data.Color = Utilities.PromptUserForValidString("Please enter a color:", ui);
            data.NumberOfWheels = Utilities.PromptUserForValidNumber("Please enter the number of wheels:", ui);
            data.MaxSpeed = Utilities.PromptUserForValidNumber("Please enter the max speed:", ui);
            data.Owner = Utilities.PromptUserForValidString("Please enter the owners first name:", ui);
            data.CylinderVolume = Utilities.PromptUserForValidNumber("Please enter the cylinder volume:", ui);

            return data;
        }
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

    public Motorcycle(MotorcycleData motorcycleData)
    {
        Data = motorcycleData;
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
