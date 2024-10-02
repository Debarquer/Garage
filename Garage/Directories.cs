using Garage.Contracts;

namespace Garage;

internal static class Directories
{
    public static string FolderSeparator { get; set; } = "\\";
    public static string DataFolder { get; set; } = "data";
    public static string GaragesFolder { get; set; } = "garages";
    public static string SavePath { get; set; } = $"data{FolderSeparator}garages";
    public static string ConfigPath { get; set; } = $"data{FolderSeparator}config.txt";

    public static void Configure(IConfig config)
    {
        DataFolder = config.DataFolder;
        GaragesFolder = config.GaragesSaveFolder;
        SavePath = Path.Combine(config.DataFolder, config.GaragesSaveFolder);
    }
}
