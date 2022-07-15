using PluginCore.PluginFrame;

namespace PluginCore.Plugins
{
	// TODO: перекинуть в PluginCore
	public class TestPlugin : IPlugin
	{
		public PluginState State => throw new NotImplementedException();

		public Guid PluginID => PluginScheme.PluginInfo[nameof(TestPlugin)];

		public async Task<PluginResult> Initialize(PluginInput pluginInput)
		{
			throw new NotImplementedException();
		}

		public async Task<PluginResult> OnRegistration()
		{
			throw new NotImplementedException();
		}

		public async Task<PluginResult> OnShutdown(PluginInput pluginInput)
		{
			throw new NotImplementedException();
		}

		public async Task<PluginResult> Run(PluginInput pluginInput)
		{
			int a = 0;
			a++;
			return null;  /*new PluginResult { ExecutionResult = a };*/
		}
	}
}
