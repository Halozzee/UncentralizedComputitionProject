using System.Net.Sockets;
using System.Text;
using MainFrame.Networking.Messaging;
using Newtonsoft.Json;
using NodeServer.Networking;

namespace MainFrame.Networking.Dispatcher
{
	public class NodeSocketContext : SocketBase
    {
        public Guid NodeId { get; set; }
        public Socket NodeSocket;
        internal BufferPool bufferAccessor = new BufferPool(ServerConfiguration.BufferAccessorSize);

        public event MessageRecievedHandler _onMessageRecievedBeforeDefault;
        public event MessageRecievedHandler _onMessageRecievedDefault;
        public event MessageRecievedHandler _onMessageRecievedAfterDefault;

        public NodeSocketContext(
            MessageRecievedHandler onMessageRecievedBeforeDefault, 
            MessageRecievedHandler onMessageRecievedDefault,
            MessageRecievedHandler onMessageRecievedAfterDefault)
		{
            _onMessageRecievedBeforeDefault += onMessageRecievedBeforeDefault;
            _onMessageRecievedDefault += onMessageRecievedDefault;
            _onMessageRecievedAfterDefault += onMessageRecievedAfterDefault;
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

                if(message.FromNodeId != null)
				{
                    NodeId = message.FromNodeId;
                }
                else
				{
                    NodeId = Guid.NewGuid();
				}

                if (!message.SkipProcessing)
                {
                    _onMessageRecievedBeforeDefault?.Invoke(this, message);
                }
                if (!message.SkipProcessing)
                {
                    _onMessageRecievedDefault?.Invoke(this, message);
                }
                if (!message.SkipProcessing)
                {
                    _onMessageRecievedAfterDefault?.Invoke(this, message);
                }

                bufferAccessor.MoveToNext();
                bufferAccessor.CleanBuffer();

                var nextBuffer = bufferAccessor.GetBuffer();

                // Start receiving data again.
                NodeSocket.BeginReceive(nextBuffer, 0, nextBuffer.Length, SocketFlags.None, ReceiveCallback, null);

                TransferMessageBuilder transferMessageBuilder = new TransferMessageBuilder();

                transferMessageBuilder
                    .WithStringData($"{NodeId} : Acknowledge")
                    .WithContainsResult(true);

                Send(transferMessageBuilder.Build());
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
            var message = JsonConvert.SerializeObject(transferMessage);
            Send(message);
        }
    }
}
