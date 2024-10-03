using Garage.Contracts;
using Newtonsoft.Json;

namespace Garage.Vehicles.Vehicles;

internal class Airplane : Vehicle
{
    public class AirplaneData : VehicleData
    {
        public int NumberOfEngines { get; set; }
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

    public Airplane(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Data = new AirplaneData();
    }


    public Airplane(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int numberOfEngines) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Data = new AirplaneData();

        NumberOfEngines = numberOfEngines;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        NumberOfEngines = Utilities.PromptUserForValidNumber("Please enter the number of engines:", ui);
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
