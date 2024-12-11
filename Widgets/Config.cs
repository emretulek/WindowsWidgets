using System.IO;
using Widgets.Common;


namespace Widgets
{
    internal partial class Config
    {
        private static Config? _instance;

        private readonly ConfigJsonStruct? settings;
        private static readonly object _lock = new();

        private Config()
        {
            if (File.Exists(App.SettingsDefaultFile))
            {
                settings = JsonFile.Read(App.SettingsDefaultFile);
            }
            else
            {
                settings = new ConfigJsonStruct
                {
                    Widgets = []
                };
            }
        }

        public static Config Instance
        {
            get
            {
                lock (_lock)
                {
                    _instance ??= new Config();
                }
                return _instance;
            }
        }

        public static void ReLoad()
        {
            _instance = null;
        }

        public WidgetDefaultStruct GetConfig(string pluginID, WidgetDefaultStruct defaultStruct)
        {
            if (settings?.Widgets != null && settings.Widgets.TryGetValue(pluginID, out var value))
            {
                return ConfigJsonStruct.JsonToConfigStruct(value);
            }

            return defaultStruct;
        }


        public void SetConfig(string PluginID, WidgetDefaultStruct configStruct)
        {
            if (string.IsNullOrEmpty(PluginID))
            {
                return;
            }

            if (settings != null)
            {
                settings.Widgets[PluginID] = ConfigJsonStruct.ConfigToJsonStruct(configStruct);
            }
        }


        public void Save()
        {
            if (settings != null)
            {
                JsonFile.Write(App.SettingsDefaultFile, settings);
            }

            ReLoad();
        }
    }
}
