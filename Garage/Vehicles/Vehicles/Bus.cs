using Garage.Contracts;
using Newtonsoft.Json;

namespace Garage.Vehicles.Vehicles;

internal class Bus : Vehicle
{
    public class BusData : VehicleData
    {
        public int NumberOfSeats { get; set; }
    }

    public int NumberOfSeats
    {
        get => ((BusData)Data).NumberOfSeats;
        private set => ((BusData)Data).NumberOfSeats = value;
    }
    public Bus() 
    { 
        Data = new BusData();
    }

    public Bus(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base()
    {
        Data = new BusData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
    }

    public Bus(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int numberOfSeats) : base()
    {
        Data = new BusData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
        NumberOfSeats = numberOfSeats;
    }

    public override void PromptUserForAdditionalData(IUI ui)
    {
        NumberOfSeats = Utilities.PromptUserForValidNumber("Please enter the number of seats:", ui);
    }

    public override string ToString()
    {
        return base.ToString() + $" Number of seats: {NumberOfSeats}";
    }

    public override void SerializeData(string line)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        this.Data = JsonConvert.DeserializeObject<BusData>(line, settings);
    }
}
