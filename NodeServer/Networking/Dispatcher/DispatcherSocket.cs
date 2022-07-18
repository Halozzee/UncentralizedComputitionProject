using NodeServer.Networking.Pipeline;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static NodeServer.Networking.SocketBase;

namespace MainFrame.Networking.Dispatcher
{

	public class DispatcherSocket
    {
        public Guid DispatcherId { get; set; }
        public List<Guid> ConnectedSocketGuids 
        {
            get 
            {
                return nodeSocketContexts.Select(x => x.NodeId).ToList();
            }
        }

        private Socket serverSocket;
        private List<NodeSocketContext> nodeSocketContexts = new List<NodeSocketContext>(); // We will only accept one socket.
        public PipelineControl<MessagePipelineDelegate> DefaultMessagePipeline { get; set; }

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

            NodeSocketContext nodeSocketContext = new NodeSocketContext(DefaultMessagePipeline);

            nodeSocketContext.NodeSocket = clientSocket;
            nodeSocketContext.DispatcherId = DispatcherId;

            nodeSocketContext.NodeSocket.BeginReceive(nodeSocketContext.bufferAccessor.GetBuffer(), 0, nodeSocketContext.bufferAccessor.GetBuffer().Length, 
                SocketFlags.None, nodeSocketContext.ReceiveCallback, null);

            nodeSocketContexts.Add(nodeSocketContext);

            serverSocket.BeginAccept(AcceptCallback, null);
        }

        public void SendToNode(Guid nodeId, TransferMessage message) 
        {
            var nodeSocketContext = nodeSocketContexts.Find(x => x.NodeId == nodeId);
            
            if (nodeSocketContext != null)
			{
                nodeSocketContext.Send(message);
            }
        }
    }
}
