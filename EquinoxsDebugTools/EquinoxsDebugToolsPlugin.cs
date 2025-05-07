using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EquinoxsDebugTools
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class EquinoxsDebugToolsPlugin : BaseUnityPlugin
    {
        internal const string MyGUID = "com.equinox.EquinoxsDebugTools";
        private const string PluginName = "EquinoxsDebugTools";
        private const string VersionString = "1.0.1";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        internal static ManualLogSource Log = new ManualLogSource(PluginName);

        // Unity Functions

        private void Awake() {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");

            foreach(KeyValuePair<ConfigDefinition, ConfigEntryBase> entry in Config) {
                Logger.LogInfo("Checking entry");
                if(entry.Value is ConfigEntry<bool> boolEntry) {
                    string key = entry.Key.Section.Replace("Mods.", "");
                    EDT.Logging.AddConfigEntry(key, boolEntry);
                    Logger.LogInfo($"Added entry: {key}, {boolEntry.Value}");
                }
            }

            EquinoxsDebugToolsConfig.CreateConfigEntries(this);

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} has loaded.");
            Log = Logger;
        }
    }
}
