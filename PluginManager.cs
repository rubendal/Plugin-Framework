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
        public List<Plugin> plugins { get; set; }
        private Func<string, bool> function;
        private PluginConfiguration config;
        private EventHandler<PluginEventArgs> startEvent;
        private EventHandler<PluginEventArgs> finishEvent;

        /// <summary>
        /// Create a <see cref="PluginManager"/> using a directory path to load plugins using <see cref="LoadPlugins"/>
        /// </summary>
        /// <param name="path">Plugin directory path</param>
        public PluginManager(string path)
        {
            this._path = path;
            config = new PluginConfiguration();
            plugins = new List<Plugin>();
            function = null;
            startEvent = new EventHandler<PluginEventArgs>((o, e) => { });
            finishEvent = new EventHandler<PluginEventArgs>((o, e) => { });
        }

        /// <summary>
        /// Create a <see cref="PluginManager"/>
        /// </summary>
        public PluginManager()
        {
            this._path = null;
            config = new PluginConfiguration();
            plugins = new List<Plugin>();
            function = null;
            startEvent = new EventHandler<PluginEventArgs>((o, e) => { });
            finishEvent = new EventHandler<PluginEventArgs>((o, e) => { });
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
        /// Set defined <see cref="EventHandler{TEventArgs}" of <see cref="PluginEventArgs"/> that will be set on loaded plugins/>
        /// </summary>
        /// <param name="started"><see cref="IPlugin.Started"/> <see cref="EventHandler{TEventArgs}"/> of <see cref="PluginEventArgs"/></param>
        /// <param name="finished"><see cref="IPlugin.Finished"/> <see cref="EventHandler{TEventArgs}"/> of <see cref="PluginEventArgs"/></param>
        public void SetEventHandlers(EventHandler<PluginEventArgs> started, EventHandler<PluginEventArgs> finished)
        {
            if(started == null)
            {
                started = new EventHandler<PluginEventArgs>((o, e) => { });
            }
            if (finished == null)
            {
                finished = new EventHandler<PluginEventArgs>((o, e) => { });
            }
            startEvent = started;
            finishEvent = finished;
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
                        if (!plugins.Contains(new Plugin(plugin, t, path)))
                        {
                            bool? keypass = function?.Invoke(plugin.GetKey());
                            if (keypass.GetValueOrDefault(true))
                            {
                                plugin.SetConfiguration(config);
                                plugin.Started += startEvent;
                                plugin.Finished += finishEvent;
                                plugins.Add(new Plugin(plugin, t, path));
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

        /// <summary>
        /// Run loaded plugins that return the name given
        /// </summary>
        /// <param name="name">Plugin name</param>
        /// <returns>true if at least a plugin is executed, false otherwise</returns>
        public bool RunPluginByName(string name)
        {
            bool exists = false;
            foreach(Plugin plugin in plugins)
            {
                if (plugin.Name == name)
                {
                    plugin.Run();
                    exists = true;
                }
            }
            return exists;
        }

        /// <summary>
        /// Run loaded plugin based on its filename without extension
        /// </summary>
        /// <param name="fileName">Filename of the loaded plugin to run, ".dll" extension will be ignored</param>
        /// <returns>true if the plugin runs, false otherwise</returns>
        public bool RunPluginByFileName(string fileName)
        {
            foreach (Plugin plugin in plugins)
            {
                if (plugin.fileName == fileName.Replace(".dll",""))
                {
                    plugin.Run();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Filter loaded plugins and get a list of plugins of the specified type
        /// </summary>
        /// <param name="pluginType">Plugin type to filter</param>
        /// <returns>List of <see cref="Plugin"/> of the specified type, returns empty List if there is no plugin</returns>
        public List<Plugin> GetPluginsByType(Type pluginType)
        {
            List<Plugin> f = new List<Plugin>();
            foreach(Plugin p in plugins)
            {
                if (GenericPlugin.CheckTypeForGenericPlugin(p.PluginType, pluginType))
                {
                    f.Add(p);
                }
            }
            return f;
        }
    }
}
