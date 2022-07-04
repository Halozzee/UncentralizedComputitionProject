using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MainFrame.Networking
{
	public class TransferMessage
	{
		public Guid FromNodeId { get; set; }
		public Guid ToNodeId { get; set; }
		public Guid PluginId { get; set; }
		public byte[] Data { get; set; }
		public bool ContainsResult { get; set; }
	}

	public static class TransferMessageExtensions
	{
		public static string GetJSONString(this TransferMessage transferMessage)
		{
			return JsonConvert.SerializeObject(transferMessage);
		}

		public static TransferMessage? GetTransferMessage(this byte[] byteMessage)
		{
			var message = Encoding.ASCII.GetString(byteMessage, 0, byteMessage.Length);
			return JsonConvert.DeserializeObject<TransferMessage>(message);
		}
	}
}
