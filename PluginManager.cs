using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Plugin_Framework.Types;

namespace Plugin_Framework
{
    public sealed class PluginManager
    {
        private string _path;
        public List<LoadedPlugin> plugins { get; set; }
        private Func<string, bool> function;
        private PluginConfiguration config;

        public PluginManager(string path)
        {
            this._path = path;
            config = new PluginConfiguration();
            plugins = new List<LoadedPlugin>();
            function = null;
        }

        public PluginManager()
        {
            this._path = null;
            config = new PluginConfiguration();
            plugins = new List<LoadedPlugin>();
            function = null;
        }

        public void SetKeyAllow(Func<string,bool> function)
        {
            this.function = function;
        }

        public void SetConfiguration(PluginConfiguration config)
        {
            this.config = config;
        }

        public bool LoadPlugin(string path)
        {
            if (File.Exists(path))
            {
                if(Path.GetExtension(path) == ".dll")
                {
                    Type[] pluginTypes;
                    try
                    {
                        pluginTypes = Assembly.Load(File.ReadAllBytes(path)).GetTypes().Where(type => type.IsSubclassOf(typeof(IPlugin)) ||
                        type.IsSubclassOf(typeof(RunnablePlugin)) ||
                        type.IsSubclassOf(typeof(MenuPlugin)) || type.IsSubclassOf(typeof(FormsPlugin)) ||
                        type.IsSubclassOf(typeof(WPFPlugin)) || type.IsSubclassOf(typeof(FilePlugin))).ToArray();
                    }
                    catch
                    {
                        return false;
                    }
                    foreach (Type t in pluginTypes)
                    {
                        IPlugin plugin = Activator.CreateInstance(t) as IPlugin;
                        if (!plugins.Contains(new LoadedPlugin(plugin, t, path)))
                        {
                            bool? keypass = function?.Invoke(plugin.GetKey());
                            if (keypass.GetValueOrDefault(true))
                            {
                                plugin.SetConfiguration(config);
                                plugins.Add(new LoadedPlugin(plugin, t, path));
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public void LoadPlugins()
        {
            if (_path != null)
            {
                LoadPluginsFromDirectory(_path);
            }
        }

        public void LoadPluginsFromDirectory(string path)
        {
            string[] dllFileNames = Directory.GetFiles(path, "*.dll");
            if (dllFileNames != null)
            {
                foreach (string file in dllFileNames)
                {
                    LoadPlugin(file);
                }
            }
        }
    }
}
