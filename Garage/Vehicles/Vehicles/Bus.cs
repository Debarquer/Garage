using Garage.Contracts;
using Newtonsoft.Json;
using static Garage.Vehicles.Vehicles.Boat;

namespace Garage.Vehicles.Vehicles;

internal class Bus : Vehicle
{
    public class BusData : VehicleData
    {
        public int NumberOfSeats { get; set; }

        public static VehicleData GetData(IUI ui, IHandler<IVehicle> garageHandler, string garageName)
        {
            BusData data = new BusData();

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
            data.NumberOfSeats = Utilities.PromptUserForValidNumber("Please enter the number of seats:", ui);

            return data;
        }
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

    public Bus(VehicleData busData)
    {
        Data = busData;
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
