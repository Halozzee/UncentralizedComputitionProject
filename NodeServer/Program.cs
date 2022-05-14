using NodeMainFrame.Networking;
using SharedServer.PluginCore;

namespace NodeMainFrame
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

		public PluginResult Run()
		{
			int a = 0;
			a++;
			return new PluginResult { ExecutionResult = a};
		}
	}

	public class Program
	{
		public static void Main()
		{
			PluginManager p = new PluginManager();
			p.RegisterPlugin(typeof(TestPlugin));

            //NodeSocket t = new NodeSocket();

            //Console.WriteLine("Test");
            //Console.ReadLine();
		}
	}
}
