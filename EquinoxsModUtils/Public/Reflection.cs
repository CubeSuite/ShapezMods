using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions.Must;
using static EquinoxsModUtils.EMULogging;

namespace EquinoxsModUtils
{
    public static partial class EMU 
    {
        /// <summary>
    /// Used to get & set private fields and invoke private methods
    /// </summary>
        public static class Reflection
        {
            /// <summary>
            /// Uses the provided info to get the value of a private field.
            /// </summary>
            /// <typeparam name="T">The class that the field belongs to</typeparam>
            /// <param name="info">The details of the field and the class</param>
            /// <returns>The value of the field if successful (it can be null), default(T) otherwise</returns>
            public static object GetPrivateField<T>(FieldSearchInfo<T> info) {
                FieldInfo field = info.Type.GetField(info.fieldName, info.Flags);
                if (field == null) {
                    LogEMUError($"Could not find the field '{info.fieldName}' under type '{info.Type}'. Aborting attempt to get value");
                    return default;
                }

                if (info.classIsStatic) return field.GetValue(null);
                else return field.GetValue(info.instance);
            }

            /// <summary>
            /// Uses the provided info to get the value of a private property.
            /// </summary>
            /// <typeparam name="T">The class that the property belongs to</typeparam>
            /// <param name="info">The details of the property and the class</param>
            /// <returns>The value of the property if successful (it can be null), default(T) otherwise</returns>
            public static object GetPrivateProperty<T>(FieldSearchInfo<T> info) {
                PropertyInfo property = info.Type.GetProperty(info.fieldName, info.Flags);
                if (property == null) {
                    LogEMUError($"Could not find the property '{info.fieldName}' under type '{info.Type}'. Aborting attempt to get value");
                    return default;
                }

                if (info.classIsStatic) return property.GetValue(null);
                else return property.GetValue(info.instance);
            }

            /// <summary>
            /// Uses the provided info to set the value of a private field.
            /// </summary>
            /// <typeparam name="T">The class that the private field belongs to</typeparam>
            /// <param name="info">The details of the field and the class</param>
            /// <param name="value"></param>
            public static void SetPrivateField<T>(FieldSearchInfo<T> info, object value) {
                FieldInfo field = info.Type.GetField(info.fieldName, info.Flags);
                if (field == null) {
                    LogEMUError($"Could not find the field '{info.fieldName}' under type '{info.Type}'. Aborting attempt to set value");
                    return;
                }

                if (info.classIsStatic) field.SetValue(null, value);
                else field.SetValue(info.instance, value);
            }

            /// <summary>
            /// Uses the provided info to invoke a private method.
            /// </summary>
            /// <param name="info">The details of the method and the class</param>
            /// <param name="args">The arguments to invoke the method with</param>
            public static void InvokePrivateMethod<T>(FieldSearchInfo<T> info, object[] args) {
                MethodInfo field = info.Type.GetMethod(info.fieldName, info.Flags);
                if (field == null) {
                    LogEMUError($"Could not find the method '{info.fieldName}' under type '{info.Type}'. Aborting attempt to invoke");
                    return;
                }

                if (info.classIsStatic) field.Invoke(null, args);
                else field.Invoke(info.instance, args);
            }
        }
    }
}
