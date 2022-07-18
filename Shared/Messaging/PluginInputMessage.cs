using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging
{
	public class PluginInputMessage
	{
		public object? PluginInputForInitialization { get; set; }
		public object? PluginInputForRun { get; set; }
		public object? PluginInputForShutdown { get; set; }
	}
}