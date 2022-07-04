using MainFrame.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeServer.Networking
{
	public class SocketBase
	{
		public delegate void MessageRecievedHandler(object sender, TransferMessage? message);
	}
}
