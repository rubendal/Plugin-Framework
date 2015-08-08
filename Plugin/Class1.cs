using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plugin
{
    public class Plugin : PluginClass.PluginClass
    {

        public override void Run()
        {
            //Do something...
            Form1 f = new Form1();
            f.ShowDialog();
        }

        
    }
}
