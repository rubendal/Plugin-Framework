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
        public abstract event EventHandler<PluginEventArgs> Started;
        public abstract event EventHandler<PluginEventArgs> Finished;

        public abstract PluginConfiguration GetConfiguration();
        public abstract string GetKey();
        public abstract string Name();
        public abstract void Run();
        public abstract void SetConfiguration(PluginConfiguration pluginConfig);

        //public abstract Type controlType { get; set; }
        public abstract T control { get; set; }

        PType IPlugin.GetPType()
        {
            return PType.Forms_GUI;
        }
    }
}
