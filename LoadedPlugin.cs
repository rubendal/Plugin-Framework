using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Plugin_Framework
{
    public class LoadedPlugin : IEquatable<LoadedPlugin>
    {
        public IPlugin plugin { get; private set; }
        public Assembly assembly { get; private set; }
        private string path;

        public LoadedPlugin(IPlugin plugin, Type type, string path)
        {
            this.plugin = plugin;
            this.assembly = type.Assembly;
            this.path = path;
        }

        public string Name
        {
            get
            {
                return plugin.Name();
            }
        }

        public bool Equals(LoadedPlugin other)
        {
            return this.Name == other.Name;
        }

        public PType GetPType()
        {
            return plugin.GetPType();
        }

        public void Run()
        {
            plugin.Run();
        }
    }
}
