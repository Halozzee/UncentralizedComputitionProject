using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginCore.PluginFrame
{
    public interface IPlugin 
    {
        Guid PluginID { get; }
        PluginState State { get; }
        Task<PluginResult> OnRegistration();
        Task<PluginResult> Initialize(PluginInput pluginInput);
        Task<PluginResult> Run(PluginInput pluginInput);
        Task<PluginResult> OnShutdown(PluginInput pluginInput);
    }
}