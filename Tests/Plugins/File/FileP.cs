using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin_Framework;
using Plugin_Framework.Types;

namespace File
{
    public class FileP : FilePlugin
    {
        public override event EventHandler<PluginEventArgs> Finished;
        public override event EventHandler<PluginEventArgs> Started;
        private PluginConfiguration config;

        public override string path { get; set; }

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
            return "File";
        }

        public override void Run()
        {
            Started(this, new PluginEventArgs(this, "Starting file"));

            string text = System.IO.File.ReadAllText(path);
            text += "\r\nAppending some text to the file";
            System.IO.File.WriteAllText(path, text);

            Finished(this, new PluginEventArgs(this, "Finished file"));
        }

        public override void SetConfiguration(PluginConfiguration pluginConfig)
        {
            config = pluginConfig;
        }
    }
}
