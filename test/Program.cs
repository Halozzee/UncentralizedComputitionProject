using MainFrame.Networking.Dispatcher;
using MainFrame.Node;
using System.Net;

namespace MainFrame.Node
{

	public class Program
	{
		public static void Main()
		{
			// --NODE--
			//NodeSocket t = new NodeSocket();

			//Console.WriteLine("Test");
			//Console.ReadLine();
			// --NODE--

			// --Dispatcher--
			DispatcherSocket s = new DispatcherSocket();
			s.StartServer(IPAddress.Parse("127.0.0.1"), 9000);
			Console.WriteLine("Test");
			Console.ReadLine();
			// --Dispatcher--
		}
	}
}
