using SharedServer.Networking;

namespace PluginCore
{
	public interface IPluginManager
    {
        public bool RegisterPlugin(Type pluginType);

        public PluginResult RunPlugin(Guid pluginId, TransferMessage message);
    }
}
