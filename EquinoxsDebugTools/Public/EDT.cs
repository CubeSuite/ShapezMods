using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxsDebugTools
{
    /// <summary>
    /// Container for all public content in EquinoxsDebugTools
    /// </summary>
    public static partial class EDT
    {
        internal static ManualLogSource Log => EquinoxsDebugToolsPlugin.Log;

        /// <summary>
        /// Checks if the provided object is null and logs if it is null
        /// </summary>
        /// <param name="obj">The object to be checked</param>
        /// <param name="name">The name of the object to add to the log line</param>
        /// <param name="shouldLog">Whether an info message should be logged if the object is not null</param>
        /// <returns>true if not null</returns>
        public static bool NullCheck(object obj, string name, bool shouldLog = false) {
            if (obj == null) {
                Log.LogWarning($"{name} is null");
                return false;
            }
            else {
                if (shouldLog) Log.LogInfo($"{name} is not null");
                return true;
            }
        }

        /// <summary>
        /// Loops through all members of 'obj' and logs its type, name and value.
        /// </summary>
        /// <param name="obj">The object to print all values of.</param>
        /// <param name="name">The name of the object to print at the start of the function.</param>
        public static void DebugObject(object obj, string name) {
            if (!NullCheck(obj, name)) {
                Log.LogError("Can't debug null object");
                return;
            }

            Dictionary<Type, string> basicTypeNames = new Dictionary<Type, string>
            {
                { typeof(bool), "bool" },
                { typeof(byte), "byte" },
                { typeof(sbyte), "sbyte" },
                { typeof(char), "char" },
                { typeof(short), "short" },
                { typeof(ushort), "ushort" },
                { typeof(int), "int" },
                { typeof(uint), "uint" },
                { typeof(long), "long" },
                { typeof(ulong), "ulong" },
                { typeof(float), "float" },
                { typeof(double), "double" },
                { typeof(decimal), "decimal" },
                { typeof(string), "string" }
            };

            Type objType = obj.GetType();
            FieldInfo[] fields = objType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            Log.LogInfo($"Debugging {objType.Name} '{name}':");
            foreach (FieldInfo field in fields) {
                string value = field.GetValue(obj)?.ToString() ?? "null";
                string type = basicTypeNames.ContainsKey(field.FieldType) ? basicTypeNames[field.FieldType] : field.FieldType.ToString();

                if (type == "char") value = $"'{value}'";
                else if (type == "string") value = $"\"{value}\"";

                Log.LogInfo($"\t{type} {field.Name} = {value}");
            }
        }
    }
}
