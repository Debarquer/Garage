namespace Garage.Contracts;

internal interface IConfig
{
    public string DataFolder { get; set; }
    public string GaragesSaveFolder { get; set; }

    public void SaveConfig(string path);
    public static abstract Config LoadConfig(string path);
}
