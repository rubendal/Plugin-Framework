# Plugin-Framework
C# NET library to implement a plugin framework

## Functionality

* **PluginManager** allows easy plugin management
```
PluginManager manager = new PluginManager();
manager.LoadPlugin(pluginPath);
```
* Can set a verification function or lambda expression to allow plugins like an API key
```
manager.SetKeyAllow((string key) => { return key == "something"; });
```
* Can pass parameters using **PluginConfiguration**
```
Dictionary<string, object> parameters = new Dictionary<string, object>();
parameters.Add("value", 30);
```

### Plugin Types
Easy plugin coding using plugin types or implement **IPlugin** for a custom one
Included types: 
* Runnable
* Menu
* Forms
* WPF
* File

### Test plugins and apps
Plugins and apps using the plugins are located on the Tests folder