using Garage.Contracts;
using Newtonsoft.Json;

namespace Garage.Vehicles;

public abstract class Vehicle : IVehicle, IPatternMatchable
{
    public abstract class VehicleData : IVehicleData, IMatchableData
    {
        public string Registration { get; set; }
        public string Color { get; set; }
        public int NumberOfWheels { get; set; }
        public int MaxSpeed { get; set; }
        public string Owner { get; set; }
    }

    public virtual IVehicleData Data { get; protected set; }
    public virtual IMatchableData MatchableData 
    {
        get => Data;
    }
    public Type MatchableObjectType => Data.GetType();

    public string Registration 
    { 
        get => Data.Registration; 
        private set => Data.Registration = value; 
    }
    public string Color
    {
        get => Data.Color;
        private set => Data.Color = value;
    }
    public int NumberOfWheels
    {
        get => Data.NumberOfWheels;
        private set => Data.NumberOfWheels = value;
    }
    public int MaxSpeed
    {
        get => Data.MaxSpeed;
        private set => Data.MaxSpeed = value;
    }
    public string Owner
    {
        get => Data.Owner;
        private set => Data.Owner = value;
    }

    public Vehicle() { }

    public Vehicle(
        string registration,
        string color,
        int numberOfWheels,
        int maxSpeed,
        string owner)
    {
        SetData(registration, color, numberOfWheels, maxSpeed, owner);
    }

    protected void SetData(string registration, string color, int numberOfWheels, int maxSpeed, string owner)
    {
        Registration = registration;
        Color = color;
        NumberOfWheels = numberOfWheels;
        MaxSpeed = maxSpeed;
        Owner = owner;
    }

    public abstract void PromptUserForAdditionalData(IUI ui);

    public override string ToString()
    {
        return $"{GetType().Name} {Registration}: {Color} {NumberOfWheels} wheels Max {MaxSpeed}kmph Owner: {Owner}";
    }

    public void Save(StreamWriter outputFile/*, StreamWriter outputFileData*/)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        string s = JsonConvert.SerializeObject(this, settings);
        outputFile.WriteLine(s);
    }

    public abstract void SerializeData(string line);
}
