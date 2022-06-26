using SharedServer.Networking;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using NodeServer;
using NodeServer.Networking;

namespace MainFrame.Networking.Dispatcher
{
	public class NodeSocketContext
    {
        public Guid NodeId { get; set; } = Guid.NewGuid();
        public Socket NodeSocket;
        internal BufferPool bufferAccessor = new BufferPool(ServerConfiguration.BufferAccessorSize);

        public delegate void MessageRecievedHandler(object sender, TransferMessage? message);
        public event MessageRecievedHandler OnMessageRecieved;

        public NodeSocketContext()
		{
			OnMessageRecieved += NodeSocketContext_OnMessageRecieved;
		}

		private void NodeSocketContext_OnMessageRecieved(object sender, TransferMessage? message)
		{
            Console.WriteLine($"{NodeId} : {message.GetJSONString()}");
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

                OnMessageRecieved?.Invoke(this, message);

                bufferAccessor.MoveToNext();
                bufferAccessor.CleanBuffer();

                var nextBuffer = bufferAccessor.GetBuffer();

                // Start receiving data again.
                NodeSocket.BeginReceive(nextBuffer, 0, nextBuffer.Length, SocketFlags.None, ReceiveCallback, null);
                Send(new TransferMessage { Data = Encoding.ASCII.GetBytes($"{NodeId} : Acknowledge") });
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
