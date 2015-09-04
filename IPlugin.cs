using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_Framework
{

    public enum PType
    {
        Not_Specified,
        Runnable,
        Menu,
        Forms_GUI,
        WPF_GUI,
        File
    }

    public interface IPlugin
    {

        void Run();

        string GetKey();

        string Name();

        PType GetPType();

        event EventHandler<PluginEventArgs> Finished;
        event EventHandler<PluginEventArgs> Started;

        PluginConfiguration GetConfiguration();
        void SetConfiguration(PluginConfiguration pluginConfig);
    }
}
