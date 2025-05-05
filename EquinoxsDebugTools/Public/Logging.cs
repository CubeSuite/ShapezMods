using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxsDebugTools
{
    public static partial class EDT 
    {
        /// <summary>
        /// Container for public functions related to logging
        /// </summary>
        public static class Logging
        {
            // Members

            private static Dictionary<string, ConfigEntry<bool>> shouldLogStatuses = new Dictionary<string, ConfigEntry<bool>>();
            private static Dictionary<string, DateTime> functionLogBlockingLimits = new Dictionary<string, DateTime>();

            // Public Functions

            /// <summary>
            /// Logs the message argument if the player has enabled the relevant config entry.
            /// </summary>
            /// <param name="category">The category of debug messages that the message belongs to. See library readme for examples</param>
            /// <param name="message">The message to log</param>
            public static void Log(string category, string message) {
                string modName = GetCallingDLL();
                string callingFunction = GetCallingFunction();

                if (!ShouldLogMessage(modName, category)) return;
                WriteToLog(category, message, callingFunction);
            }

            /// <summary>
            /// Logs the message argument if the player has enabled the relevant config entry. Blocks further calls to PacedLog() if specified.
            /// </summary>
            /// <param name="category">The category of debug messages that the mesage belongs to. See library readme for eamples.</param>
            /// <param name="message">The message to log</param>
            /// <param name="delaySeconds">How mabny seconds must pass before this function is allowed to log again. Default = 0s</param>
            public static void PacedLog(string category, string message, float delaySeconds = 0f) {
                string modName = GetCallingDLL();
                string callingFunction = GetCallingFunction();

                if (!ShouldLogMessage(modName, category)) return;
                if (functionLogBlockingLimits.TryGetValue(callingFunction, out DateTime limit) && DateTime.Now < limit) return;

                WriteToLog(category, message, callingFunction);
                if (delaySeconds > 0) {
                    functionLogBlockingLimits[callingFunction] = DateTime.Now.AddSeconds(delaySeconds);
                }
            }

            // Internal Functions

            internal static void AddConfigEntry(string key, ConfigEntry<bool> entry) {
                shouldLogStatuses.Add(key, entry);
            }

            // Private Functions

            private static string GetCallingDLL() {
                return new StackTrace().GetFrame(2).GetMethod().DeclaringType.Assembly.GetName().Name;
            }

            private static string GetCallingFunction() {
                MethodBase method = new StackTrace().GetFrame(2).GetMethod();
                return $"{method.DeclaringType.FullName}.{method.Name}()";
            }

            private static bool ShouldLogMessage(string modName, string category) {
                string key = $"{modName}.{category}";
                if (!shouldLogStatuses.ContainsKey(key)) {
                    ConfigEntry<bool> newEntry = EquinoxsDebugToolsConfig.CreateConfigEntryForCategory(modName, category);
                    shouldLogStatuses.Add(key, newEntry);
                }

                return shouldLogStatuses[key].Value;
            }

            private static void WriteToLog(string category, string message, string callingFunction) {
                string fullMessage = $"[{category}|{callingFunction}]: {message}";
                EquinoxsDebugToolsPlugin.Log.LogInfo(fullMessage);
            }
        }
    }
}
