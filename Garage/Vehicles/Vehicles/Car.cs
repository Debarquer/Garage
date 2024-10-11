using Garage.Contracts;
using Newtonsoft.Json;

namespace Garage.Vehicles.Vehicles;

public class Car : Vehicle
{
    public class CarData : VehicleData
    {
        public FuelType FuelType { get; set; }

        public static VehicleData GetData(IUI ui, IHandler<IVehicle> garageHandler, string garageName)
        {
            CarData data = new CarData();

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

            string input = Utilities.PromptUserForValidInput("Please enter the number fuel type (gas or diesel):",
            (s) => s == "gas" || s == "diesel",
                ui,
                "Please enter either gas or diesel"
            );
            switch (input)
            {
                case "gas":
                    data.FuelType = FuelType.gas;
                    break;
                case "diesel":
                    data.FuelType = FuelType.diesel;
                    break;
                default:
                    ui.PrintMessage($"{input} is not a valid fuel type.");
                    break;
            }

            return data;
        }
    }

    public FuelType FuelType
    {
        get => ((CarData)Data).FuelType;
        private set => ((CarData)Data).FuelType = value;
    }
    public Car() 
    { 
        Data = new CarData();
    }

    public Car (CarData carData)
    {
        Data = (CarData)carData;
    }

    public Car(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner) : base()
    {
        Data = new CarData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
    }

    public Car(string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner,
        FuelType fuelType) : base()
    {
        Data = new CarData();
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
        FuelType = fuelType;
    }

    public override string ToString()
    {
        return base.ToString() + $" Fuel type: {FuelType}";
    }

    public override void SerializeData(string line)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        this.Data = JsonConvert.DeserializeObject<CarData>(line, settings);
    }
}
