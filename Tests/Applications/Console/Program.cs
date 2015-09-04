using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin_Framework;
using Plugin_Framework.Types;
using System.IO;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginManager manager = new PluginManager();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("value", 30);
            manager.SetConfiguration(new PluginConfiguration(parameters));
            string runnablePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", @"Plugins\Runnable\bin\Debug\Runnable.dll"));
            string filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", @"Plugins\File\bin\Debug\File.dll"));
            manager.LoadPlugin(runnablePath);
            manager.LoadPlugin(filePath);
            foreach(LoadedPlugin r in manager.plugins)
            {
                Console.WriteLine(r.Name);
                r.plugin.Started += Plugin_Started;
                r.plugin.Finished += Plugin_Finished;
                if (r.plugin is RunnablePlugin)
                {
                    r.Run();
                }
                if (r.plugin is FilePlugin)
                {
                    FilePlugin fp = (FilePlugin)r.plugin;
                    fp.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "TextFile1.txt")); ;
                    fp.Run();
                }
            }
            Console.ReadKey();
        }

        private static void Plugin_Started(object sender, PluginEventArgs e)
        {
            Console.WriteLine(e.data);
        }

        private static void Plugin_Finished(object sender, PluginEventArgs e)
        {
            Console.WriteLine(e.data);
        }
    }
}
