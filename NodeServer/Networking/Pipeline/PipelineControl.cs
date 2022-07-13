using MainFrame.Networking.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeServer.Networking.Pipeline
{
	public delegate void MessagePipelineDelegate(object sender, TransferMessage? message);
	public class PipelineControl<T> where T : System.Delegate
	{
		public bool SkipFurther; 

		private List<T> _pipelineQueue;
		private int _counter;

		public PipelineControl()
		{
			_counter = 0;
			_pipelineQueue = new List<T>();
		}

		public void AddItem(T pipelineItem)
		{
			_pipelineQueue.Add(pipelineItem);
		}

		public void SkipItems(int countToSkip = 1)
		{
			_counter = (_counter + countToSkip) % _pipelineQueue.Count;
		}

		public T NextItem() 
		{
			return _pipelineQueue[_counter++];
		}

		public void Reset() 
		{
			_counter = 0;
			SkipFurther = false;
		}

		public bool IsEnd() 
		{
			return _counter == _pipelineQueue.Count;
		}
	}
}