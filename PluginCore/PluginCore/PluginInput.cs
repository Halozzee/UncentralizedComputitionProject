using Newtonsoft.Json;
using SharedServer.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginCore.PluginCore
{
	public class PluginInput
	{
		public byte[] InputData { get; set; }

		public static PluginInput ParseFromTransferMessage(TransferMessage message) 
		{
			return new PluginInput() { InputData = message.Data };
		}

		public T DataToObject<T>()
		{
			return JsonConvert.DeserializeObject<T>(Encoding.ASCII.GetString(InputData, 0, InputData.Length));
		}
	}
}
