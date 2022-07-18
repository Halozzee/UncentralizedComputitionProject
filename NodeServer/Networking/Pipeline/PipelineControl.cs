using Shared.Messaging;
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
		public bool PipelineStopped { get; private set; }

		private List<int> _toBeRemovedAfterExecution;
		private List<T> _pipelineQueue;
		private int _counter;

		public PipelineControl()
		{
			_counter = 0;
			_pipelineQueue = new List<T>();
			_toBeRemovedAfterExecution = new List<int>();
		}

		public void AddItem(T pipelineItem)
		{
			_pipelineQueue.Add(pipelineItem);
		}

		public void InsertItem(int i, T pipelineItem) 
		{
			_pipelineQueue.Insert(i, pipelineItem);
		}

		public void Intercept(T pipelineItem)
		{
			_pipelineQueue.Insert(_counter + 1, pipelineItem);
			_toBeRemovedAfterExecution.Add(_counter + 1);
		}

		public void SkipItems(int countToSkip = 1)
		{
			_counter = (_counter + countToSkip) % _pipelineQueue.Count;
		}

		public T NextItem() 
		{
			var pipelineItem = _pipelineQueue[_counter];

			if (_toBeRemovedAfterExecution.Contains(_counter))
			{
				_pipelineQueue.RemoveAt(_counter);
				_toBeRemovedAfterExecution.Remove(_counter);
			}

			_counter++;

			return pipelineItem;
		}

		public void StopPipeline()
		{
			PipelineStopped = true;
		}

		public void Reset() 
		{
			_counter = 0;
			PipelineStopped = false;
		}

		public bool IsEnd() 
		{
			return _counter == _pipelineQueue.Count;
		}
	}
}