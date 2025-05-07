using BepInEx;
using BepInEx.Logging;
using EquinoxsDebugTools;
using EquinoxsModUtils;
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
    public class AutoNorthPlugin : BaseUnityPlugin
    {
        internal const string MyGUID = "com.equinox.AutoNorth";
        private const string PluginName = "AutoNorth";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        internal static ManualLogSource Log = new ManualLogSource(PluginName);

        // Members

        private HUDCameraManager camera;
        private bool buildingLastFrame = false;
        private bool enabled = true;

        // Unity Functions

        private void Awake() {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");

            AutoNorthConfig.CreateConfigEntries(this);

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} has loaded.");
            Log = Logger;
        }

        private void Update() {
            if (camera == null) TryCacheCamera();

            bool isBuildingThisFrame = IsPlayerBuilding();
            if (isBuildingThisFrame && !buildingLastFrame) SetToNorth();
            buildingLastFrame = isBuildingThisFrame;

            if (AutoNorthConfig.toggleShortcut.Value.IsDown()) {
                enabled = !enabled;
                EDT.Logging.Log("Updates", $"Toggled auto-rotation to '{enabled}'");
            }
        }

        // Private Functions

        private void TryCacheCamera() {
            HUDCameraManager[] cameras = FindObjectsOfType<HUDCameraManager>();
            if (cameras.Length == 0) return;

            camera = cameras[0];
            EDT.Logging.Log("Updates", "Cached Camera");
        }

        private bool IsPlayerBuilding() {
            if (camera == null) return false;
            Player player = (Player)EMU.Reflection.GetPrivateProperty(new FieldSearchInfo<HUDCameraManager>("Player", camera));
            return player.InteractionState.PlacingAnything;
        }

        private void SetToNorth() {
            if (camera == null) {
                EDT.Logging.Log("Updates", "Didn't rotate as camera is null");
                return;
            }

            if (!enabled) {
                EDT.Logging.Log("Updates", "Didn't rotate as auto-rotation is disabled");
                return;
            }

            EMU.Reflection.SetPrivateField(new FieldSearchInfo<HUDCameraManager>("TargetRotationDegrees", camera), 0);
            EDT.Logging.Log($"Updates", "Set camera to north");
        }
    }
}
