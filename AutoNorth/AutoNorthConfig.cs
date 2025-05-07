using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoNorth
{
    public class AutoNorthConfig
    {
        // Config Entries

        internal static ConfigEntry<KeyboardShortcut> toggleShortcut;

        // Public Functions

        public static void CreateConfigEntries(BaseUnityPlugin plugin) {
            toggleShortcut = plugin.Config.Bind("General", "Toggle Shortcut", new KeyboardShortcut(KeyCode.R, KeyCode.LeftControl), "The shortcut to press to toggle auto-rotation");
        }
    }
}
