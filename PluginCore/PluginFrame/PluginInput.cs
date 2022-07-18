using Newtonsoft.Json;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginCore.PluginFrame
{
	public abstract class PluginInput
	{
		public object? InputData { get; set; }
	}
}
