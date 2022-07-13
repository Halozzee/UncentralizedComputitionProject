using MainFrame.Networking.Messaging;
using Newtonsoft.Json;
using NodeServer.Networking;
using NodeServer.Networking.Pipeline;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MainFrame.Networking.Node
{
	public class NodeSocket : SocketBase
    {
        private Socket _socket;
        private BufferPool _bufferAccessor = new BufferPool(ServerConfiguration.BufferAccessorSize);

        public PipelineControl<MessagePipelineDelegate> DefaultMessagePipeline { get; set; }

        public NodeSocket()
		{
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _socket.ReceiveBufferSize = ServerConfiguration.BufferSize;
            _socket.SendBufferSize = ServerConfiguration.BufferSize;
        }

        public void Connect(IPAddress ipAddress, int port)
        {            
            // Connect to the specified host.
            var endPoint = new IPEndPoint(ipAddress, port);
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

            if (DefaultMessagePipeline != null)
            {
                while (!DefaultMessagePipeline.IsEnd())
                {
                    if (!DefaultMessagePipeline.SkipFurther)
                    {
                        var pipelineItem = DefaultMessagePipeline.NextItem();
                        pipelineItem.Invoke(this, message);
                    }
                }

                DefaultMessagePipeline.Reset();
            }

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
