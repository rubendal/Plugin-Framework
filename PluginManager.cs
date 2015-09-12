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

        /// <summary>
        /// Create a <see cref="PluginManager"/> using a directory path to load plugins using <see cref="LoadPlugins"/>
        /// </summary>
        /// <param name="path">Plugin directory path</param>
        public PluginManager(string path)
        {
            this._path = path;
            config = new PluginConfiguration();
            plugins = new List<LoadedPlugin>();
            function = null;
        }

        /// <summary>
        /// Create a <see cref="PluginManager"/>
        /// </summary>
        public PluginManager()
        {
            this._path = null;
            config = new PluginConfiguration();
            plugins = new List<LoadedPlugin>();
            function = null;
        }

        /// <summary>
        /// Set the function to evaluate plugins keys to allow plugins to be loaded (like an API key verification)
        /// </summary>
        /// <param name="function"><see cref="Func{String, TResult}"/> of in <see cref="string"/>, out <see cref="bool"/> to use for key evaluations</param>
        public void SetKeyAllow(Func<string,bool> function)
        {
            this.function = function;
        }

        /// <summary>
        /// Set default <see cref="PluginConfiguration"/> to use in plugins
        /// </summary>
        /// <param name="config"><see cref="PluginConfiguration"/> to use</param>
        public void SetConfiguration(PluginConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Load a plugin from the specified path
        /// </summary>
        /// <param name="path">Plugin dll path</param>
        /// <returns>true if plugin was loaded successfully, false otherwise</returns>
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
                        type.IsSubclassOf(typeof(MenuPlugin)) || GenericPlugin.CheckTypeForGenericPlugin(type, typeof(FormsPlugin<>)) ||
                        GenericPlugin.CheckTypeForGenericPlugin(type, typeof(WPFPlugin<>)) || type.IsSubclassOf(typeof(FilePlugin))).ToArray();
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

        /// <summary>
        /// Load plugins from initialized path from constructor
        /// </summary>
        public void LoadPlugins()
        {
            if (_path != null)
            {
                LoadPluginsFromDirectory(_path);
            }
        }

        /// <summary>
        /// Load plugins from the specified path
        /// </summary>
        /// <param name="path">Directory path to use</param>
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
