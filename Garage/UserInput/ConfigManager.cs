using Garage.Contracts;

namespace Garage.UserInput
{
    internal class ConfigManager : CommandManager
    {
        private IUI ui;

        public ConfigManager(IUI ui)
        {
            Name = "Config";
            Aliases = ["config", "c"];

            CommandList = new CommandList(new Command[] {
               new Command(
                   "help",
                   "?command_name",
                   "Displays command help information.",
                   Help
                ),
                new Command(
                    "saveconfig",
                    "saveconfig ?dataPath ?garagesPath ?path",
                    "Saves the config.",
                    SaveConfig
                ),
                new Command(
                    "loadconfig",
                    "help ?path",
                    "Loads a config file.",
                    LoadConfig
                ),
            });

            this.ui = ui;
        }

        private void SaveConfig(string[] parameters)
        {
            string path = Directories.ConfigPath;
            string dataFolder = Directories.DataFolder;
            string garagesFolder = Directories.GaragesFolder;

            if(parameters.Length > 0)
            {
                dataFolder = parameters[0];
            }
            if(parameters.Length > 1)
            {
                garagesFolder = parameters[1];
            }
            if(parameters.Length > 2)
            {
                path = parameters[2];
            }

            Config.SaveConfig(path, dataFolder, garagesFolder);

            ui.PrintMessage($"New config saved at {path}");
        }

        private void LoadConfig(string[] parameters)
        {
            string path = Directories.ConfigPath;

            if(parameters.Length > 0)
            {
                path = parameters[0];
            }

            IConfig config = Config.LoadConfig(path);
            Directories.Configure(config);

            ui.PrintMessage($"New config loaded from {path}");
        }

        private void Help(string[] parameters) => CommandList.PrintHelp(parameters, ui);
    }
}
