namespace PluginCore.PluginFrame
{
	public interface IPluginManager
    {
        public bool RegisterPlugin(Type pluginType);

        public PluginResult RunPlugin(Guid pluginId);
    }
}
