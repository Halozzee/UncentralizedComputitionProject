using MainFrame.Networking.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MainFrame.Networking.Dispatcher
{
	public class Dispatcher
	{
		DispatcherSocket dispatcherSocket = new DispatcherSocket();

		public Dispatcher()
		{
			dispatcherSocket.SetMessageRecievedDefaultHandler(NodeSocketContext_OnMessageRecieved);
			dispatcherSocket.StartServer(IPAddress.Parse("127.0.0.1"), 9000);
		}

		private void NodeSocketContext_OnMessageRecieved(object sender, TransferMessage? message)
		{
			Console.WriteLine($"{message.FromNodeId} : {message.GetJSONString()}");
		}
	}
}
