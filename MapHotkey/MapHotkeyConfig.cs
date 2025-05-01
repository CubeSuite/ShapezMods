using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MapHotkey
{
    public class MapHotkeyConfig
    {
        // Config Entries

        //internal static ConfigEntry<int> example;
        internal static ConfigEntry<KeyCode> hotkey;

        // Public Functions

        public static void CreateConfigEntries(BaseUnityPlugin plugin) {
            hotkey = plugin.Config.Bind("General", "Hotkey", KeyCode.M, "The button to press to set the camera to map zoom level");
        }
    }
}
