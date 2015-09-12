using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_Framework
{

    public enum PType
    {
        Not_Specified,
        Runnable,
        Menu,
        Forms_GUI,
        WPF_GUI,
        File
    }

    public interface IPlugin
    {

        /// <summary>
        /// Method that contains the code the plugin will execute
        /// </summary>
        void Run();

        /// <summary>
        /// This method will return the key that will be checked
        /// </summary>
        /// <returns>Plugin key</returns>
        string GetKey();

        /// <summary>
        /// Get the plugin name
        /// </summary>
        /// <returns>Plugin name</returns>
        string Name();

        /// <summary>
        /// Get the plugin <see cref="PType"/>
        /// </summary>
        /// <returns>Plugin <see cref="PType"/></returns>
        PType GetPType();

        /// <summary>
        /// Event that should be called when plugin finishes
        /// </summary>
        event EventHandler<PluginEventArgs> Finished;
        /// <summary>
        /// Event that should be called when plugin starts
        /// </summary>
        event EventHandler<PluginEventArgs> Started;

        /// <summary>
        /// Get the <see cref="PluginConfiguration"/> set to the plugin
        /// </summary>
        /// <returns><see cref="PluginConfiguration"/> set to the plugin</returns>
        PluginConfiguration GetConfiguration();

        /// <summary>
        /// Set the <see cref="PluginConfiguration"/> to use in the plugin
        /// </summary>
        /// <param name="pluginConfig"><see cref="PluginConfiguration"/> to use in the plugin</param>
        void SetConfiguration(PluginConfiguration pluginConfig);
    }
}
