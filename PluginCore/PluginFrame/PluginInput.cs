using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginCore.PluginFrame
{
	public class PluginInput
	{
		public byte[] InputData { get; set; }

		public static PluginInput ParseFromTransferMessage() 
		{
			//TODO
			return null;
		}

		public T DataToObject<T>()
		{
			return JsonConvert.DeserializeObject<T>(Encoding.ASCII.GetString(InputData, 0, InputData.Length));
		}
	}
}
