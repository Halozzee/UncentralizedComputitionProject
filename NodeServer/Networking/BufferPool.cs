using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeServer.Networking
{
	internal class BufferPool
	{
		private List<byte[]> _buffers = new List<byte[]>();
		private Dictionary<Guid, int> _busyBuffersCommunicationIdToBufferIndex = new Dictionary<Guid, int>();

		private int _bufferCounter = 0;
		public int BufferCount { get; set; }

		public BufferPool(int buffersCount)
		{
			BufferCount = buffersCount;
			for (int i = 0; i < buffersCount; i++)
			{
				_buffers.Add(new byte[ServerConfiguration.BufferSize]);
			}
		}

		public byte[] GetBuffer(Guid communicationId)
		{
			if (_busyBuffersCommunicationIdToBufferIndex.ContainsKey(communicationId))
			{
				return _buffers[_busyBuffersCommunicationIdToBufferIndex[communicationId]];
			}
			else
			{
				return _buffers[_bufferCounter];
			}
		}

		public byte[] GetBuffer()
		{
			return _buffers[_bufferCounter];
		}

		public void CleanBuffer() 
		{
			_buffers[_bufferCounter] = new byte[ServerConfiguration.BufferSize];
		}

		public void MoveToNext() 
		{
			while (_busyBuffersCommunicationIdToBufferIndex.Values.Contains(_bufferCounter))
			{
				_bufferCounter++;

				if (_bufferCounter == BufferCount)
				{
					_bufferCounter = 0;
				}
			}
		}

		public int ReserveBuffer(Guid communicationId) 
		{
			if(_busyBuffersCommunicationIdToBufferIndex.ContainsKey(communicationId))
			{
				return _busyBuffersCommunicationIdToBufferIndex[communicationId];
			}

			for (int i = 1; i < BufferCount; i++)
			{
				if(!_busyBuffersCommunicationIdToBufferIndex.Values.Contains(i))
				{
					_busyBuffersCommunicationIdToBufferIndex.Add(communicationId, i);
					return i;
				}
			}

			return -1;
		}

		public void ReleaseBuffer(int bufferIndex)
		{
			_busyBuffersCommunicationIdToBufferIndex
				.Remove(_busyBuffersCommunicationIdToBufferIndex.Where(x => x.Value == bufferIndex).FirstOrDefault().Key);
		}
	}
}
