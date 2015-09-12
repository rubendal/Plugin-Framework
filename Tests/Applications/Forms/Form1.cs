using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plugin_Framework;
using Plugin_Framework.Types;
using System.IO;

namespace Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool evaluateKey(string key)
        {
            return key == "something";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PluginManager manager = new PluginManager();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("value", 30);
            manager.SetConfiguration(new PluginConfiguration(parameters));
            //manager.SetKeyAllow(evaluateKey);
            manager.SetKeyAllow((string key) => { return key == "something"; });
            string runnablePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", @"Plugins\Runnable\bin\Debug\Runnable.dll"));
            string menuPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", @"Plugins\Menu\bin\Debug\Menu.dll"));
            string formPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", @"Plugins\Form\bin\Debug\Form.dll"));
            manager.LoadPlugin(runnablePath);
            manager.LoadPlugin(menuPath);
            manager.LoadPlugin(formPath);
            foreach (LoadedPlugin r in manager.plugins)
            {
                Console.WriteLine(r.Name);
                r.plugin.Started += Plugin_Started;
                r.plugin.Finished += Plugin_Finished;
                if (r.plugin is RunnablePlugin)
                {
                    r.Run();
                }
                //if (r.plugin is MenuPlugin)
                if(GenericPlugin.CheckTypeForGenericPlugin(r.PluginType, typeof(MenuPlugin)))
                {
                    MenuPlugin mp = (MenuPlugin)r.plugin;
                    mp.menuStrip = this.formMenuStrip;
                    mp.Run();
                }
                if (GenericPlugin.CheckTypeForGenericPlugin(r.PluginType,typeof(FormsPlugin<>)))
                {
                    Type t = GenericPlugin.GetGenericClass(r.PluginType);
                    if (t == typeof(Form))
                    {
                        FormsPlugin<Form> fp = (FormsPlugin<Form>)r.plugin;
                        fp.control = this;
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
