using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Plugin_Framework.Types
{
    public abstract class WPFPlugin : IPlugin
    {
        public abstract event EventHandler<PluginEventArgs> Started;
        public abstract event EventHandler<PluginEventArgs> Finished;

        public abstract PluginConfiguration GetConfiguration();
        public abstract string GetKey();
        public abstract string Name();
        public abstract void Run();
        public abstract void SetConfiguration(PluginConfiguration pluginConfig);

        public abstract Type controlType { get; set; }
        public abstract Control control { get; set; }

        public abstract Window window { get; set; }

        PType IPlugin.GetPType()
        {
            return PType.WPF_GUI;
        }
    }
}
