using MainFrame.Networking.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MainFrame.Networking.Node
{
	public class Node
	{
		NodeSocket nodeSocket;
		public Node()
		{
			
		}

		public void Connect(IPAddress iPAddress, int port) 
		{
			nodeSocket = new NodeSocket(iPAddress, port);
			nodeSocket.SetMessageRecievedDefaultHandler(T_OnMessageRecieved);

			Console.WriteLine("Test");
			Console.ReadLine();

			while (true)
			{
				string input = Console.ReadLine();
				nodeSocket.Send(new TransferMessage() { Data = Encoding.ASCII.GetBytes(input) });
			}
		}

		private static void T_OnMessageRecieved(object sender, TransferMessage? message)
		{
			Console.WriteLine(message.GetJSONString());
		}
	}
}
