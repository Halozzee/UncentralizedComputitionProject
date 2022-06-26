﻿using PluginCore.PluginCore;
using SharedServer.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginCore
{
    public interface IPlugin 
    {
        Guid PluginID { get; }
        PluginState State { get; }
        PluginResult OnRegistration();
        PluginResult Initialize(PluginInput pluginInput);
        PluginResult Run(PluginInput pluginInput);
        PluginResult OnShutdown(PluginInput pluginInput);
    }
}