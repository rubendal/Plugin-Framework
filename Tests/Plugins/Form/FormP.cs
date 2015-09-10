using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plugin_Framework.Types;
using Plugin_Framework;

namespace FormP
{
    public class FormP : FormsPlugin<Form>
    {
        public override Form control { get; set; }

        //public override Type controlType { get; set; } = typeof(Form);

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
            return "something";
        }

        public override string Name()
        {
            return "Form";
        }

        public override void Run()
        {
            Started(this, new PluginEventArgs(this, "Starting form"));

            textbox = new TextBox();
            textbox.Name = "TextBox";
            textbox.Size = new System.Drawing.Size(100, 20);
            textbox.Location = new System.Drawing.Point(12, 40);

            Button button = new Button();
            button.Name = "Button";
            button.Size = new System.Drawing.Size(50, 20);
            button.Location = new System.Drawing.Point(122, 40);
            button.Text = "Press";
            button.Click += Button_Click;

            control.Controls.Add(textbox);
            control.Controls.Add(button);

            Finished(this, new PluginEventArgs(this, "Finished form"));
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
