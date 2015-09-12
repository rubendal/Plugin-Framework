using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace Plugin_Framework.Types
{
    public abstract class FormsPlugin<T> : IPlugin where T : Control
    {
        /// <summary>
        /// Event that should be called when plugin starts
        /// </summary>
        public abstract event EventHandler<PluginEventArgs> Started;
        /// <summary>
        /// Event that should be called when plugin finishes
        /// </summary>
        public abstract event EventHandler<PluginEventArgs> Finished;

        /// <summary>
        /// Get the <see cref="PluginConfiguration"/> set to the plugin
        /// </summary>
        /// <returns><see cref="PluginConfiguration"/> set to the plugin</returns>
        public abstract PluginConfiguration GetConfiguration();
        /// <summary>
        /// This method will return the key that will be checked
        /// </summary>
        /// <returns>Plugin key</returns>
        public abstract string GetKey();
        /// <summary>
        /// Get the plugin name
        /// </summary>
        /// <returns>Plugin name</returns>
        public abstract string Name();
        /// <summary>
        /// Method that contains the code the plugin will execute
        /// </summary>
        public abstract void Run();
        /// <summary>
        /// Set the <see cref="PluginConfiguration"/> to use in the plugin
        /// </summary>
        /// <param name="pluginConfig"><see cref="PluginConfiguration"/> to use in the plugin</param>
        public abstract void SetConfiguration(PluginConfiguration pluginConfig);

        /// <summary>
        /// Get the plugin <see cref="PType"/>
        /// </summary>
        /// <returns>Plugin <see cref="PType"/></returns>
        PType IPlugin.GetPType()
        {
            return PType.Forms_GUI;
        }

        //public abstract Type controlType { get; set; }

        /// <summary>
        /// <see cref="T"/> to use in <see cref="FormsPlugin{T}"/>
        /// </summary>
        public abstract T control { get; set; }

        
    }
}
