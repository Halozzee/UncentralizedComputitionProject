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
			MainFrame.Networking.Node.Node n = new MainFrame.Networking.Node.Node();
			n.Connect(IPAddress.Parse("127.0.0.1"), 9000);
		}
	}
}
