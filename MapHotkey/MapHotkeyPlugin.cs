using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EquinoxsModUtils;
using EquinoxsDebugTools;

namespace MapHotkey
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class MapHotkeyPlugin : BaseUnityPlugin
    {
        internal const string MyGUID = "com.equinox.MapHotkey";
        private const string PluginName = "MapHotkey";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        internal static ManualLogSource Log = new ManualLogSource(PluginName);

        private HUDCameraManager camera;

        // Unity Functions

        private void Awake() {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");

            MapHotkeyConfig.CreateConfigEntries(this);

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} has loaded.");
            Log = Logger;
        }

        public void Update() {
            if (!UnityInput.Current.GetKeyDown(MapHotkeyConfig.hotkey.Value)) return;
            if (camera == null && !GetCamera()) return;

            ToggleCameraZoomLevel();
        }

        // Private Functions

        private bool GetCamera() {
            HUDCameraManager[] cameras = FindObjectsOfType<HUDCameraManager>();
            if (cameras.Length == 0) return false;
            
            camera = cameras[0];
            return transform;
        }

        private void ToggleCameraZoomLevel() {
            FieldSearchInfo<HUDCameraManager> viewportFieldInfo = new FieldSearchInfo<HUDCameraManager>(
                fieldName: "Viewport",
                instance: camera
            );

            Viewport viewport = (Viewport)EMU.Reflection.GetPrivateField(viewportFieldInfo);
            float oldValue = viewport.TargetZoom;

            if (viewport.TargetZoom < 400 || viewport.TargetZoom >= 6400) viewport.TargetZoom = 400;
            else if (viewport.TargetZoom < 1600) viewport.TargetZoom = 1600;
            else if (viewport.TargetZoom < 6400) viewport.TargetZoom = 6400;

            EMU.Reflection.SetPrivateField(viewportFieldInfo, viewport);
            EDT.Logging.Log("Zoom Updates", $"Updated TargetZoom from '{oldValue}' to '{viewport.TargetZoom}'");
        }
    }
}
