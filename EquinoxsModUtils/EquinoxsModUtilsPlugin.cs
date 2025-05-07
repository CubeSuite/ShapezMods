using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EquinoxsModUtils
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class EquinoxsModUtilsPlugin : BaseUnityPlugin
    {
        internal const string MyGUID = "com.equinox.EquinoxsModUtils";
        private const string PluginName = "EquinoxsModUtils";
        private const string VersionString = "1.1.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        internal static ManualLogSource Log = new ManualLogSource(PluginName);

        // Unity Functions

        private void Awake() {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");

            ApplyPatches();

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
