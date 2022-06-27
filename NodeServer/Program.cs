using MainFrame.Networking;
using MainFrame.Networking.Node;
using System.Net;
using System.Text;

namespace MainFrame.Node
{

	public class Program
	{
		public static void Main()
		{
			//PluginManager p = new PluginManager();
			//p.RegisterPlugin(typeof(TestPlugin));

			// --NODE--
			NodeSocket t = new NodeSocket(IPAddress.Parse("127.0.0.1"), 9000);

			Console.WriteLine("Test");
			Console.ReadLine();

			t.OnMessageRecieved += T_OnMessageRecieved;

			while (true)
			{
				string input = Console.ReadLine();
				t.Send(new TransferMessage() { Data = Encoding.ASCII.GetBytes(input)});
			}

			// --NODE--

			// --Dispatcher--
			//DispatcherSocket s = new DispatcherSocket();
			//s.StartServer();
			//Console.WriteLine("Test");
			//Console.ReadLine();
			// --Dispatcher--
		}

		private static void T_OnMessageRecieved(object sender, TransferMessage? message)
		{
			Console.WriteLine(message.GetJSONString());
		}
	}
}
