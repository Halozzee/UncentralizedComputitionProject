using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainFrame.Networking.Messaging
{
	public class TransferMessageBuilder
	{
		TransferMessage _resultTransferMessage = new TransferMessage();

		public TransferMessageBuilder FromNodeId(Guid id) 
		{
			_resultTransferMessage.FromNodeId = id;
			return this;
		}

		public TransferMessageBuilder ToNodeId(Guid id)
		{
			_resultTransferMessage.ToNodeId = id;
			return this;
		}

		public TransferMessageBuilder WithPluginId(Guid id)
		{
			_resultTransferMessage.PluginId = id;
			return this;
		}

		public TransferMessageBuilder WithJsonData(string jsonData)
		{
			_resultTransferMessage.Data = Encoding.ASCII.GetBytes(jsonData);
			return this;
		}

		public TransferMessageBuilder WithStringData(string stringData)
		{
			_resultTransferMessage.Data = Encoding.ASCII.GetBytes(stringData);
			return this;
		}

		public TransferMessageBuilder WithObjectToJsonData(object objectToSerialize)
		{
			_resultTransferMessage.Data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(objectToSerialize));
			return this;
		}

		public TransferMessageBuilder WithByteData(byte[] data)
		{
			_resultTransferMessage.Data = data;
			return this;
		}

		public TransferMessageBuilder WithContainsResult(bool containsResult)
		{
			_resultTransferMessage.ContainsResult = containsResult;
			return this;
		}

		public TransferMessage Build() 
		{
			return _resultTransferMessage;
		}
	}
}
