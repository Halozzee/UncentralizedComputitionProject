using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeServer.Plugins
{
	// TODO: перекинуть в PluginCore
	internal static class PluginScheme
	{
		internal static Dictionary<int, Guid> PluginInfo = new Dictionary<int, Guid>();

		static PluginScheme()
		{
			PluginInfo.Add(0, Guid.Parse("de44fede-fb25-4e6a-8ed9-7a8a322b03a7"));
		}
	}
}
