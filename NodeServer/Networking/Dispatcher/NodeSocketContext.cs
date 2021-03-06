using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using NodeServer.Networking;
using NodeServer.Networking.Pipeline;
using Shared.Messaging;

namespace MainFrame.Networking.Dispatcher
{
	public class NodeSocketContext : SocketBase
    {
        public Guid DispatcherId { get; set; }
        public Guid NodeId { get; set; }
        public Socket NodeSocket;
        internal BufferPool bufferAccessor = new BufferPool(ServerConfiguration.BufferAccessorSize);

        public NodeSocketContext(PipelineControl<MessagePipelineDelegate> messagePipelineQueue)
		{
            DefaultMessagePipeline = messagePipelineQueue;
        }

        private void SendCallback(IAsyncResult AR)
        {
            NodeSocket.EndSend(AR);
        }

        public void ReceiveCallback(IAsyncResult AR)
        {
			try
			{
                int received = NodeSocket.EndReceive(AR);

                if (received == 0)
                {
                    return;
                }

                var message = bufferAccessor.GetBuffer().GetTransferMessage();

                if (message.FromNodeId != Guid.Empty)
                {
                    NodeId = message.FromNodeId;
                }

                ProcessDefaultMessagePipeline(message);

                bufferAccessor.MoveToNext();
                bufferAccessor.CleanBuffer();

                var nextBuffer = bufferAccessor.GetBuffer();

                // Start receiving data again.
                NodeSocket.BeginReceive(nextBuffer, 0, nextBuffer.Length, SocketFlags.None, ReceiveCallback, null);
            }
			catch (Exception ex)
			{

			}
        }

        public void Send(string data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            Send(byteData);
        }

        public void Send(byte[] data)
        {
            // Begin sending the data to the remote device.  
            NodeSocket.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(SendCallback), NodeSocket);
        }

        public void Send(TransferMessage transferMessage)
        {
            if(DispatcherId != Guid.Empty)
			{
                transferMessage.FromNodeId = NodeId;
            }

            if (NodeId != Guid.Empty)
            {
                transferMessage.ToNodeId = NodeId;
            }

            var message = JsonConvert.SerializeObject(transferMessage);
            Send(message);
        }
    }
}
