using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin_Framework;
using Plugin_Framework.Types;

namespace Runnable
{
    public class Runnable : RunnablePlugin
    {
        public override event EventHandler<PluginEventArgs> Finished;
        public override event EventHandler<PluginEventArgs> Started;
        private PluginConfiguration config;

        public override PluginConfiguration GetConfiguration()
        {
            return config;
        }

        public override string GetKey()
        {
            return "";
        }

        public override string Name()
        {
            return "Runnable";
        }

        public override void Run()
        {

            Started(this, new PluginEventArgs(this, "Starting runnable"));

            int p = 20;
            if (config.GetParameter("value") != null)
            {
                p = Convert.ToInt32(config.GetParameter("value"));
            }
            
            for(int i = 0; i < p; i++)
            {
                Console.WriteLine(i);
            }

            Finished(this, new PluginEventArgs(this, "Finished runnable"));
        }

        public override void SetConfiguration(PluginConfiguration pluginConfig)
        {
            config = pluginConfig;
        }
    }
}
