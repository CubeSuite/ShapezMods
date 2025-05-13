using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAsteroids.Patches
{
    internal class HUDShapeResourcesVisualizationPatch
    {
        [HarmonyPatch(typeof(HUDShapeResourcesVisualization), nameof(HUDShapeResourcesVisualization.DrawResource))]
        [HarmonyPrefix]
        static bool PatchName(ShapeDefinition definition) {
            if (!CleanAsteroidsPlugin.enabled) return true;

            ShapePart[] parts = definition.Layers[0].Parts;
            if (parts[0].ToString() != parts[1].ToString()) return false;
            if (parts[0].ToString() != parts[2].ToString()) return false;
            if (parts[0].ToString() != parts[3].ToString()) return false;

            return true;
        }
    }
}
