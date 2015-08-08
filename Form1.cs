using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace PluginForm
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Plugins list
        /// </summary>
        static List<PluginClass.PluginClass> plugins;
        /// <summary>
        /// Plugin's Assembly list
        /// </summary>
        static List<Assembly> assemblies;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            plugins = new List<PluginClass.PluginClass>();
            assemblies = new List<Assembly>();
            CheckFolder();
            try
            {
                GetPluginFiles();
                foreach (PluginClass.PluginClass plugin in plugins)
                {
                    //Add plugin to menu
                    int i = plugins.IndexOf(plugin);
                    ToolStripMenuItem m = new ToolStripMenuItem();
                    m.Name = "Plugin" + i;
                    m.Text = string.Format("{0} v{1}", getName(assemblies[i]),getVersion(assemblies[i]));
                    m.Click += delegate
                    {
                        MessageBox.Show(string.Format("Plugin: {0}\nVersion: {1}\nDescripcion: {2}",getName(assemblies[i]),getVersion(assemblies[i]),getInfo(assemblies[i])));
                    };
                    pluginsMenu.DropDownItems.Add(m);

                    try
                    {
                        plugin.Run();
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Check if plugins directory exists, if not create the directory
        /// </summary>
        private void CheckFolder()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Plugins"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Plugins");
            }
        }

        /// <summary>
        /// Get dll files from the directory
        /// </summary>
        private void GetPluginFiles()
        {
            string[] dllFileNames = null;
            if (Directory.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Plugins"))
            {
                dllFileNames = Directory.GetFiles(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Plugins", "*.dll");
                if (dllFileNames != null)
                {
                    foreach (string file in dllFileNames)
                    {
                        AddPluginFrom(file);
                    }
                }

            }
        }
        /// <summary>
        /// Load plugin from path
        /// </summary>
        /// <param name="file">Plugin dll path</param>
        private void AddPluginFrom(string file)
        {
            Type[] pluginTypes;
            try
            {
                pluginTypes = Assembly.Load(File.ReadAllBytes(file)).GetTypes().Where(type => type.IsSubclassOf(typeof(PluginClass.PluginClass))).ToArray();
            }
            catch (Exception e)
            {
                return;
            }
            foreach (Type t in pluginTypes)
            {
                PluginClass.PluginClass plugin = Activator.CreateInstance(t) as PluginClass.PluginClass;
                plugins.Add(plugin);
                assemblies.Add(t.Assembly);
            }
        }


        private string getName(Assembly assembly)
        {
            return GetAttributeValue<AssemblyTitleAttribute>(assembly, a => a.Title, Path.GetFileNameWithoutExtension(assembly.CodeBase));
        }

        private string getVersion(Assembly assembly)
        {
            string v = assembly.GetName().Version.ToString().TrimEnd('.', '0');
            if (v.Length == 1)
            {
                v += ".0";
            }
            return v;
        }

        private string getAuthor(Assembly assembly)
        {
            string s = GetAttributeValue<AssemblyCompanyAttribute>(assembly, a => a.Company);
            if (!string.IsNullOrEmpty(s))
            {
                return s;
            }
            else
            {
                return GetAttributeValue<AssemblyCopyrightAttribute>(assembly, a => a.Copyright);
            }
        }

        private string getInfo(Assembly assembly)
        {
            return GetAttributeValue<AssemblyDescriptionAttribute>(assembly, a => a.Description);
        }

        private string GetAttributeValue<TAttr>(Assembly assembly, Func<TAttr,
          string> resolveFunc, string defaultResult = "") where TAttr : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(TAttr), false);
            if (attributes.Length > 0)
                return resolveFunc((TAttr)attributes[0]);
            else
                return defaultResult;
        }
    }
}
