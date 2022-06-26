using NodeServer;
using SharedServer.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MainFrame.Networking.Dispatcher
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

            clientSocket.ReceiveBufferSize = ServerConfiguration.BufferSize;
            clientSocket.SendBufferSize = ServerConfiguration.BufferSize;

            NodeSocketContext nodeSocketContext = new NodeSocketContext();
            nodeSocketContext.Buffer = new byte[clientSocket.ReceiveBufferSize];

            nodeSocketContext.NodeSocket = clientSocket;

            nodeSocketContext.NodeSocket.BeginReceive(nodeSocketContext.Buffer, 0, nodeSocketContext.Buffer.Length, 
                SocketFlags.None, nodeSocketContext.ReceiveCallback, null);

            ss.Add(nodeSocketContext);

            nodeSocketContext.Send(new TransferMessage() { Data = Encoding.ASCII.GetBytes($"{cntr++}")});

            serverSocket.BeginAccept(AcceptCallback, null);
        }
    }
}
