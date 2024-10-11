using Garage.Contracts;
using Newtonsoft.Json;
using static Garage.Vehicles.Vehicles.Motorcycle;

namespace Garage.Vehicles.Vehicles;

internal class Unicycle : Vehicle
{
    public class UnicycleData : VehicleData
    {
        public int Height { get; set; }

        public static VehicleData GetData(IUI ui, IHandler<IVehicle> garageHandler, string garageName)
        {
            UnicycleData data = new UnicycleData();

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
            data.Height = Utilities.PromptUserForValidNumber("Please enter the height:", ui);

            return data;
        }
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

    public Unicycle(VehicleData unicycleData)
    {
        Data = unicycleData;
    }

public Unicycle(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base()
    {
        Data = new UnicycleData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
    }

    public Unicycle(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        int height) : base()
    {
        Data = new UnicycleData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
        Height = height;
    }

    public override void SerializeData(string line)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        this.Data = JsonConvert.DeserializeObject<UnicycleData>(line, settings);
    }
}
