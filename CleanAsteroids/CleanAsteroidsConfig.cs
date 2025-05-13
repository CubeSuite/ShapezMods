using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CleanAsteroids
{
    public class CleanAsteroidsConfig
    {
        // Config Entries

        //internal static ConfigEntry<int> example;
        internal static ConfigEntry<KeyboardShortcut> toggle;

        // Public Functions

        public static void CreateConfigEntries(BaseUnityPlugin plugin) {
            toggle = plugin.Config.Bind("General", "Toggle Shortcut", new KeyboardShortcut(KeyCode.H, KeyCode.LeftControl), "The shortcut to press to toggle hiding of non-pure shape asteroids.");
        }
    }
}
