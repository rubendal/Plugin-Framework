using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Plugin_Framework
{
    /// <summary>
    /// Contains loaded plugins data
    /// </summary>
    public class Plugin : IEquatable<Plugin>
    {
        public IPlugin plugin { get; private set; }
        public Assembly assembly { get; private set; }
        public Type PluginType { get; private set; }
        private string path;
        internal string fileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(path);
            }
        }

        public Plugin(IPlugin plugin, Type type, string path)
        {
            this.plugin = plugin;
            this.PluginType = type;
            this.assembly = type.Assembly;
            this.path = path;
        }

        /// <summary>
        /// Get loaded plugin name
        /// </summary>
        public string Name
        {
            get
            {
                return plugin.Name();
            }
        }

        /// <summary>
        /// Checks if a plugin is equal depending on the name
        /// </summary>
        /// <param name="other">Plugin to compare</param>
        /// <returns>true if both plugins names are equal, false otherwise</returns>
        public bool Equals(Plugin other)
        {
            return this.Name == other.Name;
        }

        /// <summary>
        /// Returns plugin <see cref="PType"/>
        /// </summary>
        /// <returns>Plugin class <see cref="PType"/></returns>
        public PType GetPType()
        {
            return plugin.GetPType();
        }

        /// <summary>
        /// Execute the plugin <see cref="IPlugin.Run"/> method
        /// </summary>
        public void Run()
        {
            plugin.Run();
        }
    }
}
