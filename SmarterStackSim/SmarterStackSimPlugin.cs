using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SmarterStackSim.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SmarterStackSim
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class SmarterStackSimPlugin : BaseUnityPlugin
    {
        internal const string MyGUID = "com.equinox.SmarterStackSim";
        private const string PluginName = "SmarterStackSim";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        internal static ManualLogSource Log = new ManualLogSource(PluginName);

        // Unity Functions

        private void Awake() {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");

            ApplyPatches();

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} has loaded.");
            Log = Logger;
        }

        // Private Functions

        private void ApplyPatches() {
            Harmony.CreateAndPatchAll(typeof(VirtualStackerSimulationPatch));
        }
    }
}
