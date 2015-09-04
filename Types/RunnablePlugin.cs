using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_Framework.Types
{
    public abstract class RunnablePlugin : IPlugin
    {
        public abstract event EventHandler<PluginEventArgs> Started;
        public abstract event EventHandler<PluginEventArgs> Finished;

        public abstract PluginConfiguration GetConfiguration();
        public abstract string GetKey();
        public abstract string Name();
        public abstract void Run();
        public abstract void SetConfiguration(PluginConfiguration pluginConfig);

        PType IPlugin.GetPType()
        {
            return PType.Runnable;
        }
    }
}
