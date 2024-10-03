using Garage.Contracts;
using Newtonsoft.Json;

namespace Garage.Vehicles.Vehicles;

public class Car : Vehicle
{
    public class CarData : VehicleData
    {
        public FuelType FuelType { get; set; }
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

    public override void PromptUserForAdditionalData(IUI ui)
    {
        string input = Utilities.PromptUserForValidInput("Please enter the number fuel type (gas or diesel):",
            (s) => s == "gas" || s == "diesel",
            ui,
            "Please enter either gas or diesel"
            );
        switch (input)
        {
            case "gas":
                FuelType = FuelType.gas;
                break;
            case "diesel":
                FuelType = FuelType.diesel;
                break;
            default:
                ui.PrintMessage($"{input} is not a valid fuel type.");
                return;
        }
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
