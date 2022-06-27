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
        public async Task<string> RegisterPlugin(Type pluginType) 
        {
            IPlugin instance = (IPlugin)Activator.CreateInstance(pluginType);

            if (instance == null)
            {
                return "Failed to get instance";
            }

            var result = await instance.OnRegistration();

            if (!result.IsSuccess)
            {
                return await result.BuildErrorMessage();
            }

            if (instance.PluginID == Guid.Empty)
            {
                throw new ArgumentNullException("Plugin ID has to be provided!");
            }

            plugins.Add(instance);
            return await result.BuildSuccessMessage();
        }

		public async Task<PluginResult> RunPlugin(
            Guid pluginId,
            PluginInput pluginInputForInitialization,
            PluginInput pluginInputForRun,
            PluginInput pluginInputForShutdown)
		{
			var pluginToRun = plugins.Find(x => x.PluginID == pluginId);

			var initializeResult = await pluginToRun.Initialize(pluginInputForInitialization);
			var runResult = await pluginToRun.Run(pluginInputForRun);
			var shutdownResult = await pluginToRun.OnShutdown(pluginInputForShutdown);

			return runResult;
		}
	}
}
