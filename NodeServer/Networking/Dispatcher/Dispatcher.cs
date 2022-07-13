using MainFrame.Networking.Dispatcher;
using MainFrame.Networking.Messaging;
using NodeServer.Networking.Pipeline;
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
			PipelineControl<MessagePipelineDelegate> pipelineQueue = new PipelineControl<MessagePipelineDelegate>();

			pipelineQueue.AddItem(BeforeDefault);
			pipelineQueue.AddItem(Default);
			pipelineQueue.AddItem(AfterDefault);

			dispatcherSocket.DefaultMessagePipeline = pipelineQueue;

			dispatcherSocket.StartServer(IPAddress.Parse("127.0.0.1"), 9000);
		}

		private void BeforeDefault(object sender, TransferMessage? message)
		{
			Console.WriteLine($"{message.FromNodeId} 1 : {message.GetJSONString()}");

			TransferMessageBuilder builder = new TransferMessageBuilder();

			builder = builder.FromNodeId(Guid.NewGuid()).WithStringData("Acknowledge");

			(sender as NodeSocketContext).Send(builder.Build());
			(sender as NodeSocketContext).DefaultMessagePipeline.SkipFurther = true;
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
