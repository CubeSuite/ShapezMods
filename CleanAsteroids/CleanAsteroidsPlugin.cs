using BepInEx;
using BepInEx.Logging;
using CleanAsteroids.Patches;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CleanAsteroids
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class CleanAsteroidsPlugin : BaseUnityPlugin
    {
        internal const string MyGUID = "com.equinox.CleanAsteroids";
        private const string PluginName = "CleanAsteroids";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        internal static ManualLogSource Log = new ManualLogSource(PluginName);

        // Members

        internal static bool enabled = true;

        // Unity Functions

        private void Awake() {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");

            ApplyPatches();
            CleanAsteroidsConfig.CreateConfigEntries(this);

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} has loaded.");
            Log = Logger;
        }

        private void Update() {
            if (CleanAsteroidsConfig.toggle.Value.IsDown()) {
                enabled = !enabled;
            }
        }

        // Private Functions

        private void ApplyPatches() {
            Harmony.CreateAndPatchAll(typeof(HUDShapeResourcesVisualizationPatch));
        }
    }
}
