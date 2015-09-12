using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_Framework
{
    public class PluginEventArgs : EventArgs
    {
        public IPlugin source { get; set; }
        public object data { get; set; }

        /// <summary>
        /// <see cref="EventArgs"/> used by Plugins events
        /// </summary>
        /// <param name="source">Plugin source</param>
        /// <param name="data">data to pass</param>
        public PluginEventArgs(IPlugin source, object data)
        {
            this.source = source;
            this.data = data;
        }
    }
}
