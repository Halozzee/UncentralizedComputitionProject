using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedServer.PluginCore
{
    public interface IPlugin 
    {
        Guid PluginID { get; }
        PluginState State { get; }
        PluginResult Run();
        PluginResult Initialize();
        PluginResult OnRegistration();
        PluginResult OnShutdown();
    }
}