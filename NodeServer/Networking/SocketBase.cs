using NodeServer.Networking.Pipeline;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeServer.Networking
{
	public class SocketBase
	{
        public PipelineControl<MessagePipelineDelegate> DefaultMessagePipeline { get; set; }
        public void ProcessDefaultMessagePipeline(TransferMessage message) 
		{
            if (DefaultMessagePipeline != null)
            {
                while (!DefaultMessagePipeline.IsEnd())
                {
                    var pipelineItem = DefaultMessagePipeline.NextItem();
                    if (!DefaultMessagePipeline.PipelineStopped)
                    {
                        pipelineItem.Invoke(this, message);
                    }
                    else
					{
                        break;
					}
                }

                DefaultMessagePipeline.Reset();
            }
        }
	}
}
