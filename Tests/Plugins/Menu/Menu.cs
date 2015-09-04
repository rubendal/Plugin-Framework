using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plugin_Framework.Types;
using Plugin_Framework;

namespace Menu
{
    public class Menu : MenuPlugin
    {
        public override MenuStrip menuStrip { get; set; }

        public override event EventHandler<PluginEventArgs> Finished;
        public override event EventHandler<PluginEventArgs> Started;
        private PluginConfiguration config;

        public override PluginConfiguration GetConfiguration()
        {
            return config;
        }

        public override string GetKey()
        {
            return "0000";
        }

        public override string Name()
        {
            return "Menu";
        }

        public override void Run()
        {
            Started(this, new PluginEventArgs(this, "Starting menu"));

            if (menuStrip != null)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(Name());
                item.Name = Name();
                ToolStripMenuItem dropdown = new ToolStripMenuItem("Do stuff");
                dropdown.Click += Dropdown_Click;
                item.DropDownItems.Add(dropdown);
                menuStrip.Items.Add(item);
            }

            Finished(this, new PluginEventArgs(this, "Finished menu"));
        }

        private void Dropdown_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello from menu added with plugin");
        }

        public override void SetConfiguration(PluginConfiguration pluginConfig)
        {
            config = pluginConfig;
        }
    }
}
