using MainFrame.Node.Plugins;
using PluginCore;

namespace MainFrame.Node
{

	public class Program
	{
		public static void Main()
		{
			PluginManager p = new PluginManager();
			p.RegisterPlugin(typeof(TestPlugin));

			// --NODE--
			//NodeSocket t = new NodeSocket();

			//Console.WriteLine("Test");
			//Console.ReadLine();
			// --NODE--

			// --Dispatcher--
			//DispatcherSocket s = new DispatcherSocket();
			//s.StartServer();
			//Console.WriteLine("Test");
			//Console.ReadLine();
			// --Dispatcher--
		}
	}
}
