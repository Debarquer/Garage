using Garage.Contracts;
using Newtonsoft.Json;

namespace Garage.Vehicles.Vehicles;

internal class Unicycle : Vehicle
{
    public class UnicycleData : VehicleData
    {
        public int Height { get; set; }
    }

    public int Height
    {
        get => ((UnicycleData)Data).Height;
        private set => ((UnicycleData)Data).Height = value;
    }

    public Unicycle() 
    { 
        Data = new UnicycleData();
    }

    public Unicycle(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Data = new UnicycleData();
    }

    public Unicycle(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int height) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Data = new UnicycleData();

        Height = height;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        Height = Utilities.PromptUserForValidNumber("Please enter the height:", ui);
    }

    public override void SerializeData(string line)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        this.Data = JsonConvert.DeserializeObject<UnicycleData>(line, settings);
    }
}
