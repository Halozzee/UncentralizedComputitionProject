using MainFrame.Networking;
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

			Program p = new Program();

			// --Dispatcher--
			Dispatcher d = new Dispatcher();
			Console.WriteLine("Test");
			Console.ReadLine();
			// --Dispatcher--
		}
	}
}
