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

        /// <summary>
        /// Initialize an empty <see cref="PluginConfiguration"/>
        /// </summary>
        public PluginConfiguration()
        {
            this.parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initialize a <see cref="PluginConfiguration"/> with parameters
        /// </summary>
        /// <param name="parameters"></param>
        public PluginConfiguration(Dictionary<string, object> parameters)
        {
            this.parameters = parameters;
        }

        /// <summary>
        /// Add a parameter to the <see cref="PluginConfiguration"/>
        /// </summary>
        /// <param name="key">string key to use</param>
        /// <param name="value">value to store</param>
        /// <returns></returns>
        public bool AddParameter(string key, object value)
        {
            if (!parameters.ContainsKey(key))
            {
                parameters.Add(key, value);
            }
            return false;
        }

        /// <summary>
        /// Get the parameter with the specified key
        /// </summary>
        /// <param name="key">key to use</param>
        /// <returns>if <see cref="PluginConfiguration"/> contains a value for the specified key it will return the value, if not it returns null</returns>
        public object GetParameter(string key)
        {
            if (parameters.ContainsKey(key))
            {
                return parameters[key];
            }
            return null;
        }

        /// <summary>
        /// Get all keys stored
        /// </summary>
        /// <returns>string[] with all keys contained</returns>
        public string[] GetKeys()
        {
            return parameters.Keys.ToArray();
        }
    }
}
