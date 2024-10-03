using Garage.Contracts;
using Newtonsoft.Json;

namespace Garage.Vehicles.Vehicles;

internal class Boat : Vehicle
{
    public class BoatData : VehicleData
    {
        public int Length { get; set; }
    }

    public int Length
    {
        get => ((BoatData)Data).Length;
        private set => ((BoatData)Data).Length = value;
    }
    public Boat() 
    { 
        Data = new BoatData();
    }

    public Boat(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Data = new BoatData();
    }

    public Boat(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int length) : base(registration, color, numberOfWheels, maxSpeed, owner)
    {
        Data = new BoatData();

        Length = length;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        Length = Utilities.PromptUserForValidNumber("Please enter the length: ", ui);
    }

    public override string ToString()
    {
        return base.ToString() + $" Length: {Length}";
    }

    public override void SerializeData(string line)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        this.Data = JsonConvert.DeserializeObject<BoatData>(line, settings);
    }
}
