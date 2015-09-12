# Plugin-Framework

## Functionality

* **PluginManager** allows easy plugin management
```
PluginManager manager = new PluginManager();
manager.LoadPlugin(pluginPath);

//Using a directory for Plugins
PluginManager manager = new PluginManager(pluginDirectoryPath);
manager.LoadPlugins();
```
* Can set a verification function or lambda expression to allow plugins like an API key
```
//Simple verification
manager.SetKeyAllow((string key) => { return key == "something"; });

//Verification using SQL database
manager.SetKeyAllow((string key) => {
  SqlConnection sql_con = new SqlConnection(DBConnectionString);
  sql_con.Open();
  SqlCommand sql_com = sql_con.CreateCommand();
  sql_com.CommandText = "SELECT * FROM keytable WHERE key = '" + key + "'";
  SqlDataReader sql_reader = sql_com.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
  if (sql_reader.Read())
  {
    return true;
  }
  return false;
});
```
* Can pass parameters using **PluginConfiguration**
```
Dictionary<string, object> parameters = new Dictionary<string, object>();
parameters.Add("value", 30);
parameters.Add("phrase", "Hello World");
parameters.Add("bytes", new byte[] { 1, 2, 3 });
```

### Plugin Types
Easy plugin coding using abstract plugin types or implement **IPlugin** for a custom one

Included types: 
* Runnable
* Menu (Windows Forms MenuStrip)
* Forms`<`System.Windows.Forms.Control`>`
* WPF`<`System.Windows.UIElement`>`
* File

### Test plugins and apps
Plugins and apps using the plugins are located on the Tests folder

## NuGet
NuGet is available here https://www.nuget.org/packages/rubendal.PluginFramework
