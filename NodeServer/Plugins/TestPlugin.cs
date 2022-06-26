using PluginCore;

namespace MainFrame.Node.Plugins
{
	public class TestPlugin : IPlugin
	{
		public PluginState State => throw new NotImplementedException();

		public Guid PluginID => throw new NotImplementedException();

		public PluginResult Initialize()
		{
			throw new NotImplementedException();
		}

		public PluginResult OnRegistration()
		{
			throw new NotImplementedException();
		}

		public PluginResult OnShutdown()
		{
			throw new NotImplementedException();
		}

		public PluginResult Run()
		{
			int a = 0;
			a++;
			return new PluginResult { ExecutionResult = a};
		}
	}
}
