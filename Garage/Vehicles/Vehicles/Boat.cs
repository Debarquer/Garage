using Garage.Contracts;
using Newtonsoft.Json;
using static Garage.Vehicles.Vehicles.Airplane;

namespace Garage.Vehicles.Vehicles;

internal class Boat : Vehicle
{
    public class BoatData : VehicleData
    {
        public int Length { get; set; }

        public static VehicleData GetData(IUI ui, IHandler<IVehicle> garageHandler, string garageName)
        {
            BoatData data = new BoatData();

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
            data.Length = Utilities.PromptUserForValidNumber("Please enter the length: ", ui);

            return data;
        }
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

    public Boat(VehicleData boatData)
    {
        Data = boatData;
    }

    public Boat(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base()
    {
        Data = new BoatData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
    }

    public Boat(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int length) : base()
    {
        Data = new BoatData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);

        Length = length;
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
