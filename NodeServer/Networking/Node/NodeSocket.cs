using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MainFrame.Networking.Node
{
	public class NodeSocket 
	{
        private Socket socket;
        private BufferPool bufferAccessor = new BufferPool(ServerConfiguration.BufferAccessorSize);

        public delegate void MessageRecievedHandler(object sender, TransferMessage? message);
        public event MessageRecievedHandler OnMessageRecieved;

        public NodeSocket(IPAddress ipAddress, int port)
		{
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Connect to the specified host.
            var endPoint = new IPEndPoint(ipAddress, port);

            socket.ReceiveBufferSize = ServerConfiguration.BufferSize;
            socket.SendBufferSize = ServerConfiguration.BufferSize;

            socket.BeginConnect(endPoint, ConnectCallback, null);
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            int received = socket.EndReceive(AR);

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
            socket.BeginReceive(nextBuffer, 0, nextBuffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void ConnectCallback(IAsyncResult AR)
        {
            socket.EndConnect(AR);
            bufferAccessor.GetBuffer();
            socket.BeginReceive(bufferAccessor.GetBuffer(), 0, bufferAccessor.GetBuffer().Length, SocketFlags.None, ReceiveCallback, null);

            Send(new TransferMessage { Data = Encoding.ASCII.GetBytes("hello!")});
        }

        private void SendCallback(IAsyncResult AR)
        {
            socket.EndSend(AR);
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
            socket.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(SendCallback), socket);
        }

        public void Send(TransferMessage transferMessage)
        {
            var message = JsonConvert.SerializeObject(transferMessage);
            Send(message);
        }
    }
}
