using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxsModUtils
{
    /// <summary>
    /// Container for the info required to search for a private field
    /// </summary>
    /// <typeparam name="T">The class that contains the private field</typeparam>
    public class FieldSearchInfo<T>
    {
        // Members

        internal string fieldName;
        internal T instance;
        internal bool classIsStatic;
        internal bool fieldIsStatic;
        private BindingFlags flags;

        // Properties

        internal Type Type => typeof(T);
        internal BindingFlags Flags => BindingFlags.NonPublic | (fieldIsStatic ? BindingFlags.Static : BindingFlags.Instance);

        // Constructors

        /// <summary>
        /// Creates an instance of FieldSearchInfo
        /// </summary>
        /// <param name="fieldName">The name of the field / method to search for</param>
        /// <param name="instance">The instance of the class to interact with. Use null if class is static</param>
        /// <param name="fieldIsStatic">Whether the field / method is static</param>
        /// <param name="classIsStatic">Whether the class that contains the field / method is static.</param>
        public FieldSearchInfo(string fieldName, T instance, bool fieldIsStatic = false, bool classIsStatic = false) {
            this.fieldName = fieldName;
            this.instance = instance;
            this.fieldIsStatic = fieldIsStatic;
            this.fieldIsStatic = fieldIsStatic;
        }
    }
}
