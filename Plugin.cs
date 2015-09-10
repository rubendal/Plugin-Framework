using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_Framework
{
    public static class Plugin
    {

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
