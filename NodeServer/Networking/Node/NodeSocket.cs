using Newtonsoft.Json;
using NodeServer.Networking;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MainFrame.Networking.Node
{
	public class NodeSocket : SocketBase
    {
        private Socket _socket;
        private BufferPool _bufferAccessor = new BufferPool(ServerConfiguration.BufferAccessorSize);

        private MessageRecievedHandler _onMessageRecieved;

        public void SetMessageRecievedDefaultHandler(MessageRecievedHandler onMessageRecieved)
        {
            this._onMessageRecieved = onMessageRecieved;
        }

        public NodeSocket(IPAddress ipAddress, int port)
		{
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Connect to the specified host.
            var endPoint = new IPEndPoint(ipAddress, port);

            _socket.ReceiveBufferSize = ServerConfiguration.BufferSize;
            _socket.SendBufferSize = ServerConfiguration.BufferSize;

            _socket.BeginConnect(endPoint, ConnectCallback, null);
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            int received = _socket.EndReceive(AR);

            if (received == 0)
            {
                return;
            }

            var message = _bufferAccessor.GetBuffer().GetTransferMessage();

            _onMessageRecieved?.Invoke(this, message);

            _bufferAccessor.MoveToNext();
            _bufferAccessor.CleanBuffer();

            var nextBuffer = _bufferAccessor.GetBuffer();

            // Start receiving data again.
            _socket.BeginReceive(nextBuffer, 0, nextBuffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void ConnectCallback(IAsyncResult AR)
        {
            _socket.EndConnect(AR);
            _bufferAccessor.GetBuffer();
            _socket.BeginReceive(_bufferAccessor.GetBuffer(), 0, _bufferAccessor.GetBuffer().Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void SendCallback(IAsyncResult AR)
        {
            _socket.EndSend(AR);
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
            _socket.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(SendCallback), _socket);
        }

        public void Send(TransferMessage transferMessage)
        {
            var message = JsonConvert.SerializeObject(transferMessage);
            Send(message);
        }
    }
}
