using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace PluginCore.PluginFrame
{
	public class PluginManager : IPluginManager
    {
        private List<IPlugin> plugins = new List<IPlugin>();
        public bool RegisterPlugin(Type pluginType) 
        {
            IPlugin instance = (IPlugin)Activator.CreateInstance(pluginType);

            if (instance == null)
            {
                return false;
            }

            var result = instance.OnRegistration();

            if (!result.IsSuccess)
            {
                return false;
            }

            if (instance.PluginID == Guid.Empty)
            {
                throw new ArgumentNullException("Plugin ID has to be provided!");
            }

            plugins.Add(instance);
            return true;
        }

		public PluginResult RunPlugin(Guid pluginId)
		{
            //var pluginToRun = plugins.Find(x => x.PluginID == pluginId);
            //PluginInput pluginInput = PluginInput.ParseFromTransferMessage(message);

            //var initializeResult = pluginToRun.Initialize(pluginInput);
            //var runResult = pluginToRun.Run(pluginInput);
            //var shutdownResult = pluginToRun.OnShutdown(pluginInput);

            //return runResult;
            return null;
        }
	}
}
