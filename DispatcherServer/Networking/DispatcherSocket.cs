using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherMainFrame.Networking
{

	public class DispatcherSocket
    {
        private Socket serverSocket;
        private List<NodeSocketContext> ss = new List<NodeSocketContext>(); // We will only accept one socket.
        int cntr = 0;

        public void StartServer(IPAddress ipAddress, int port)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(new IPEndPoint(ipAddress, port));
            serverSocket.Listen();
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket clientSocket = serverSocket.EndAccept(AR);

            NodeSocketContext nodeSocketContext = new NodeSocketContext();
            nodeSocketContext.Buffer = new byte[clientSocket.ReceiveBufferSize];

            nodeSocketContext.NodeSocket = clientSocket;

            var sendData = Encoding.ASCII.GetBytes($"{cntr++}");

            nodeSocketContext.NodeSocket.BeginSend(sendData, 0, sendData.Length, SocketFlags.None, nodeSocketContext.SendCallback, null);

            nodeSocketContext.NodeSocket.BeginReceive(nodeSocketContext.Buffer, 0, nodeSocketContext.Buffer.Length, 
                SocketFlags.None, nodeSocketContext.ReceiveCallback, null);

            ss.Add(nodeSocketContext);

            serverSocket.BeginAccept(AcceptCallback, null);
        }
    }
}
