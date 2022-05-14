using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NodeMainFrame.Networking
{
	public class NodeSocket 
	{
        private Socket socket;
        private byte[] buffer;

		public NodeSocket(IPAddress ipAddress, int port)
		{
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Connect to the specified host.
            var endPoint = new IPEndPoint(ipAddress, port);
            socket.BeginConnect(endPoint, ConnectCallback, null);
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            int received = socket.EndReceive(AR);

            if (received == 0)
            {
                return;
            }

            string message = Encoding.ASCII.GetString(buffer);

			Console.WriteLine(message);
            // Start receiving data again.
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void ConnectCallback(IAsyncResult AR)
        {
            socket.EndConnect(AR);
            buffer = new byte[socket.ReceiveBufferSize];
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);

            var sendData = Encoding.ASCII.GetBytes($"Hello");

            socket.Send(sendData);
        }

        private void SendCallback(IAsyncResult AR)
        {
            socket.EndSend(AR);
        }
    }
}
