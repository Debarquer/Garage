using Garage.Contracts;
using Newtonsoft.Json;
using static Garage.Vehicles.Vehicles.Car;

namespace Garage.Vehicles.Vehicles;

internal class Airplane : Vehicle
{
    public class AirplaneData : VehicleData
    {
        public int NumberOfEngines { get; set; }

        public static VehicleData GetData(IUI ui, IHandler<IVehicle> garageHandler, string garageName)
        {
            AirplaneData data = new AirplaneData();

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
            data.NumberOfEngines = Utilities.PromptUserForValidNumber("Please enter the number of engines:", ui);

            return data;
        }
    }

    public int NumberOfEngines
    {
        get => ((AirplaneData)Data).NumberOfEngines;
        private set => ((AirplaneData)Data).NumberOfEngines = value;
    }

    public Airplane() 
    { 
        Data = new AirplaneData();
    }

    public Airplane(VehicleData airplaneData)
    {
        Data = airplaneData;
    }

    public Airplane(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base()
    {
        Data = new AirplaneData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
    }


    public Airplane(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int numberOfEngines) : base()
    {
        Data = new AirplaneData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);

        NumberOfEngines = numberOfEngines;
    }

    public override string ToString()
    {
        return base.ToString() + $" Number of engines: {NumberOfEngines}";
    }

    public override void SerializeData(string line)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        this.Data = JsonConvert.DeserializeObject<AirplaneData>(line, settings);
    }
}
