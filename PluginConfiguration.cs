using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_Framework
{
    public sealed class PluginConfiguration
    {
        private Dictionary<string, object> parameters;

        public PluginConfiguration()
        {
            this.parameters = new Dictionary<string, object>();
        }

        public PluginConfiguration(Dictionary<string, object> parameters)
        {
            this.parameters = parameters;
        }

        public object GetParameter(string key)
        {
            if (parameters.ContainsKey(key))
            {
                return parameters[key];
            }
            return null;
        }

        public string[] GetKeys()
        {
            return parameters.Keys.ToArray();
        }
    }
}
