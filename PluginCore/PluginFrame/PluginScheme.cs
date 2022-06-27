using PluginCore.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginCore.PluginFrame
{
	// TODO: перекинуть в PluginCore
	internal static class PluginScheme
	{
		internal static Dictionary<string, Guid> PluginInfo = new Dictionary<string, Guid>();

		static PluginScheme()
		{
			PluginInfo.Add(nameof(TestPlugin), Guid.Parse("de44fede-fb25-4e6a-8ed9-7a8a322b03a7"));
		}
	}
}
