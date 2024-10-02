using Garage.Contracts;

namespace Garage;

internal static class Directories
{
    private static string dataFolder { get; set; } = "data";
    private static string garagesFolder { get; set; } = "garages";

    public static string FolderSeparator { get; set; } = "\\";
    public static string SavePath { get; set; } = $"data{FolderSeparator}garages";
    public static string ConfigPath { get; set; } = $"data{FolderSeparator}config.txt";

    public static void Configure(IConfig config)
    {
        dataFolder = config.DataFolder;
        garagesFolder = config.GaragesSaveFolder;
        SavePath = Path.Combine(config.DataFolder, config.GaragesSaveFolder);
    }

    public static IConfig SetConfigData(IConfig config)
    {
        config.DataFolder = dataFolder;
        config.GaragesSaveFolder = garagesFolder;

        return config;
    }
}
