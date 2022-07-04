using MainFrame.Networking.Dispatcher;
using MainFrame.Networking.Messaging;
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
			dispatcherSocket.SetMessageRecievedBeforeDefaultHandler(BeforeDefault);
			dispatcherSocket.SetMessageRecievedDefaultHandler(Default);
			dispatcherSocket.SetMessageRecievedAfterDefaultHandler(AfterDefault);
			dispatcherSocket.StartServer(IPAddress.Parse("127.0.0.1"), 9000);
		}

		private void BeforeDefault(object sender, TransferMessage? message)
		{
			Console.WriteLine($"{message.FromNodeId} 1 : {message.GetJSONString()}");
		}
		private void Default(object sender, TransferMessage? message)
		{
			Console.WriteLine($"{message.FromNodeId} 2 : {message.GetJSONString()}");
		}
		private void AfterDefault(object sender, TransferMessage? message)
		{
			Console.WriteLine($"{message.FromNodeId} 3 : {message.GetJSONString()}");
		}
	}
}
