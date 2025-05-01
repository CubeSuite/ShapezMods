using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxsModUtils
{
    internal static class EMULogging
    {
        internal static void LogEMUInfo(string message) {
            EquinoxsModUtilsPlugin.Log.LogInfo(message);
        }

        internal static void LogEMUWarning(string message) {
            EquinoxsModUtilsPlugin.Log.LogWarning(message);
        }

        internal static void LogEMUError(string message) {
            EquinoxsModUtilsPlugin.Log.LogError(message);
        }
    }
}
