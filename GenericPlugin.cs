using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_Framework
{
    /// <summary>
    /// Methods to extract generic Plugins types
    /// </summary>
    public static class GenericPlugin
    {
        /// <summary>
        /// Check if plugin is a type that is generic
        /// </summary>
        /// <param name="check">Plugin to check (use PluginType property)</param>
        /// <param name="pluginType">Plugin type to check</param>
        /// <returns></returns>
        public static bool CheckTypeForGenericPlugin(Type check, Type pluginType)
        {
            if (check != null && check != typeof(object))
            {
                Type t = check.IsGenericType ? check.GetGenericTypeDefinition() : check;
                if (pluginType == t)
                {
                    return true;
                }
                check = check.BaseType;
                return CheckTypeForGenericPlugin(check, pluginType);
            }
            return false;
        }

        /// <summary>
        /// Obtain the type of a parameterized Plugin
        /// </summary>
        /// <param name="genericType">Plugin Type (use PluginType)</param>
        /// <returns><see cref="Type"/> of the parameterized plugin, if it is not generic returns null</returns>
        public static Type GetGenericClass(Type genericType)
        {
            try
            {
                return genericType.BaseType.GetGenericArguments()[0];
            }
            catch
            {

            }
            return null;
        }
    }
}
