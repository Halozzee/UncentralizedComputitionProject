namespace PluginCore.PluginFrame
{
	public interface IPluginManager
    {
        public Task<PluginResult> RegisterPlugin(Type pluginType);

        public Task<PluginResult> RunPlugin(
            Guid pluginId,
            PluginInput pluginInputForInitialization,
            PluginInput pluginInputForRun,
            PluginInput pluginInputForShutdown);
    }
}
