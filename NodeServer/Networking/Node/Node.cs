using MainFrame.Networking.Messaging;
using MainFrame.Networking.Node;
using NodeServer.Networking.Pipeline;
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
			nodeSocket = new NodeSocket();

			PipelineControl<MessagePipelineDelegate> pipelineQueue = new PipelineControl<MessagePipelineDelegate>();

			pipelineQueue.AddItem(BeforeDefault);
			pipelineQueue.AddItem(Default);
			pipelineQueue.AddItem(AfterDefault);
			pipelineQueue.AddItem(AfterDefault2);

			nodeSocket.DefaultMessagePipeline = pipelineQueue;
			nodeSocket.Connect(iPAddress, port);

			Console.WriteLine("Test");
			Console.ReadLine();

			while (true)
			{
				string input = Console.ReadLine();

				TransferMessageBuilder transferMessageBuilder = new TransferMessageBuilder();

				transferMessageBuilder
					.WithStringData(input);

				nodeSocket.Send(transferMessageBuilder.Build());
			}
		}
		private static void BeforeDefault(object sender, TransferMessage? message)
		{
			Console.WriteLine(1);
			Console.WriteLine(message.GetJSONString());
		}
		private static void Default(object sender, TransferMessage? message)
		{
			Console.WriteLine(2);
			Console.WriteLine(message.GetJSONString());
		}
		private static void AfterDefault(object sender, TransferMessage? message)
		{
			(sender as NodeSocket).DefaultMessagePipeline.SkipFurther = false;
			Console.WriteLine(3);
			Console.WriteLine(message.GetJSONString());
		}
		private static void AfterDefault2(object sender, TransferMessage? message)
		{
			Console.WriteLine(4);
			Console.WriteLine(message.GetJSONString());
		}
	}
}
