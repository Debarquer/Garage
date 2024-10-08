﻿using Garage.Contracts;

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
                    "?path",
                    "Loads a config file.",
                    LoadConfig
                ),
            });

            this.ui = ui;
        }

        private void SaveConfig(string[] parameters)
        {
            IConfig config = new Config();
            string path = Directories.ConfigPath;
            
            config = Directories.SetConfigData(config);

            if(parameters.Length > 0)
            {
                config.DataFolder = parameters[0];
            }
            if(parameters.Length > 1)
            {
                config.GaragesSaveFolder = parameters[1];
            }
            if(parameters.Length > 2)
            {
                path = parameters[2];
            }

            config.SaveConfig(path);

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
            ui.PrintMessage($"Data folder: {config.DataFolder}");
            ui.PrintMessage($"Garages save folder: {config.GaragesSaveFolder}");
        }

        private void Help(string[] parameters) => CommandList.PrintHelp(parameters, ui);
    }
}
