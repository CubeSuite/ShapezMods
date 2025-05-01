using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoNorth
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class AutoNorth : BaseUnityPlugin
    {
        internal const string MyGUID = "com.equinox.AutoNorth";
        private const string PluginName = "AutoNorth";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        internal static ManualLogSource Log = new ManualLogSource(PluginName);

        // Unity Functions

        private void Awake() {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");

            ApplyPatches();
            AutoNorthConfig.CreateConfigEntries(this);

            // ToDo: Add loading code here

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} has loaded.");
            Log = Logger;
        }

        private void Update() {
            // ToDo: Delete if not needed
        }

        // Private Functions

        private void ApplyPatches() {
            // Harmony.CreateAndPatchAll(typeof(ExampleClassPatch))
        }
    }
}
