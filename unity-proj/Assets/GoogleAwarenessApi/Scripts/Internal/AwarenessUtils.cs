using System;
using System.Linq;
using NinevaStudios.AwarenessApi;

namespace GoogleAwarenessApi.Scripts.Internal
{
	public class AwarenessUtils
	{
		public static string CommaJoin(Weather.Condition[] items)
		{
			return String.Join(",", items.Select(x => x.ToString()).ToArray());
		}
	}
}