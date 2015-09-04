using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Plugin_Framework.Types;
using Plugin_Framework;

namespace WPF
{
    public class WPF : WPFPlugin
    {
        public override Control control { get; set; }

        public override Type controlType { get; set; } = typeof(Window);

        public override Window window { get; set; }

        public override event EventHandler<PluginEventArgs> Finished;
        public override event EventHandler<PluginEventArgs> Started;
        private PluginConfiguration config;

        private TextBox textbox;

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
            return "Form";
        }

        public override void Run()
        {
            Started(this, new PluginEventArgs(this, "Starting WPF"));

            textbox = new TextBox();
            textbox.Name = "TextBox";
            textbox.Width = 100;
            textbox.Height = 40;
            textbox.Margin = new Thickness(0, 12, -100, -100);

            Button button = new Button();
            button.Name = "Button";
            button.Width = 75;
            button.Height = 40;
            button.Margin = new Thickness(120, 12, -100, -100);
            button.Content = "Press";
            button.Click += Button_Click;

            Grid grid = window.Content as Grid;
            grid.Children.Add(textbox);
            grid.Children.Add(button);

            Finished(this, new PluginEventArgs(this, "Finished WPF"));
        }

        private void Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(textbox.Text);
        }

        public override void SetConfiguration(PluginConfiguration pluginConfig)
        {
            config = pluginConfig;
        }
    }
}
