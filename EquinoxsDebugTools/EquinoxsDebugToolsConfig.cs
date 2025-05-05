using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxsDebugTools
{
    public class EquinoxsDebugToolsConfig
    {
        // Config Entries

        //internal static ConfigEntry<int> example;
        internal static ConfigEntry<bool> developerMode;
        internal static BaseUnityPlugin pluginInstance;

        // Public Functions

        internal static void CreateConfigEntries(BaseUnityPlugin plugin) {
            pluginInstance = plugin;
            developerMode = plugin.Config.Bind("EDT", "Developer Mode", false, new ConfigDescription("When enabled, new config entires will default to true"));
        }

        internal static ConfigEntry<bool> CreateConfigEntryForCategory(string modName, string category) {
            ConfigEntry<bool> newEntry = pluginInstance.Config.Bind(
                $"Mods.{modName}",
                $"Debug {category}",
                developerMode.Value,
                $"Whether debug messages should be logged for {modName} - {category}"
            );

            return newEntry;
        }
    }
}
