using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
namespace PluginClass
{
    interface IPlugin
    {
        void Run();
        string getName();
        string getVersion();
        string getAuthor();
        string getInfo();
    }

    public abstract class PluginClass : IPlugin
    {

        public abstract void Run();

        public string getName()
        {
            return GetAttributeValue<AssemblyTitleAttribute>(a => a.Title, Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase));
        }

        public string getVersion()
        {
            string v = Assembly.GetExecutingAssembly().GetName().Version.ToString().TrimEnd('.', '0');
            if (v.Length == 1)
            {
                v += ".0";
            }
            return v;
        }

        public string getAuthor()
        {
            string s = GetAttributeValue<AssemblyCompanyAttribute>(a => a.Company);
            if (!string.IsNullOrEmpty(s))
            {
                return s;
            }
            else
            {
                return GetAttributeValue<AssemblyCopyrightAttribute>(a => a.Copyright);
            }
        }

        public string getInfo()
        {
            return GetAttributeValue<AssemblyDescriptionAttribute>(a => a.Description);
        }

        private string GetAttributeValue<TAttr>(Func<TAttr,
          string> resolveFunc, string defaultResult = "") where TAttr : Attribute
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(TAttr), false);
            if (attributes.Length > 0)
                return resolveFunc((TAttr)attributes[0]);
            else
                return defaultResult;
        }
    }

}