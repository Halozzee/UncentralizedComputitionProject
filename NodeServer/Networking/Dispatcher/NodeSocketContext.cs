using System.Net.Sockets;

namespace MainFrame.Networking.Dispatcher
{
	public class NodeSocketContext
    {
        public Socket NodeSocket;
        public byte[] Buffer;

        public void SendCallback(IAsyncResult AR)
        {
            NodeSocket.EndSend(AR);
        }

        public void ReceiveCallback(IAsyncResult AR)
        {
            int received = NodeSocket.EndReceive(AR);

            if (received == 0)
            {
                return;
            }

            // Start receiving data again.
            NodeSocket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, ReceiveCallback, null);
        }
    }
}
