using Garage.Contracts;
using Newtonsoft.Json;

namespace Garage;

internal class Config : IConfig
{
    public string DataFolder { get; set; }
    public string GaragesSaveFolder { get; set; }

    public static void SaveConfig(string path, string dataFolder, string garagesSaveFolder)
    {
        IConfig config = new Config();
        config.GaragesSaveFolder = dataFolder;
        config.DataFolder = garagesSaveFolder;
        using (StreamWriter outputFile = new StreamWriter(path))
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string s = JsonConvert.SerializeObject(config, settings);
            outputFile.WriteLine(s);
        }
    }

    public static Config LoadConfig(string path)
    {
        Config config;
        using (StreamReader inputStreamReader = new StreamReader(path))
        {
            string line = inputStreamReader.ReadLine();
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            config = JsonConvert.DeserializeObject<Config>(line, settings);

            Directories.Configure(config);
        }

        return config;
    }
}
