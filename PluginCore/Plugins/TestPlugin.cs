using PluginCore.PluginFrame;

namespace PluginCore.Plugins
{
	// TODO: перекинуть в PluginCore
	public class TestPlugin : IPlugin
	{
		public PluginState State => throw new NotImplementedException();

		public Guid PluginID => PluginScheme.PluginInfo[0];

		public PluginResult Initialize(PluginInput pluginInput)
		{
			return null;
		}

		public PluginResult OnRegistration()
		{
			return null;
		}

		public PluginResult OnShutdown(PluginInput pluginInput)
		{
			return null;
		}

		public PluginResult Run(PluginInput pluginInput)
		{
			int a = 0;
			a++;
			return new PluginResult { ExecutionResult = a };
		}
	}
}
