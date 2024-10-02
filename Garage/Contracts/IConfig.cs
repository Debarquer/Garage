namespace Garage.Contracts;

internal interface IConfig
{
    public string DataFolder { get; set; }
    public string GaragesSaveFolder { get; set; }

    public abstract static void SaveConfig(string path, string dataFolder, string garagesSaveFolder);
    public abstract static Config LoadConfig(string path);
}
