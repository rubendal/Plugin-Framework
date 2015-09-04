using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.IO;
using Plugin_Framework;
using Plugin_Framework.Types;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PluginManager manager = new PluginManager();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("value", 30);
            manager.SetConfiguration(new PluginConfiguration(parameters));
            string runnablePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", @"Plugins\Runnable\bin\Debug\Runnable.dll"));
            string wpfPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", @"Plugins\WPF\bin\Debug\WPF.dll"));
            manager.LoadPlugin(runnablePath);
            manager.LoadPlugin(wpfPath);
            foreach (LoadedPlugin r in manager.plugins)
            {
                Console.WriteLine(r.Name);
                r.plugin.Started += Plugin_Started;
                r.plugin.Finished += Plugin_Finished;
                if (r.plugin is RunnablePlugin)
                {
                    r.Run();
                }
                
                if (r.plugin is WPFPlugin)
                {
                    WPFPlugin fp = (WPFPlugin)r.plugin;
                    if (fp.controlType == typeof(Window))
                    {
                        fp.window = this;
                        fp.Run();
                    }
                }
            }
        }

        private void Plugin_Started(object sender, PluginEventArgs e)
        {
            Console.WriteLine(e.data);
        }

        private void Plugin_Finished(object sender, PluginEventArgs e)
        {
            Console.WriteLine(e.data);
        }
    }
}
