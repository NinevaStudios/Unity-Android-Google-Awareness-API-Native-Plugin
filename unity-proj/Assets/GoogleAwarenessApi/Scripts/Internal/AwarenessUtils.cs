using System;
using System.Collections.Generic;
using System.Linq;
using NinevaStudios.AwarenessApi;

namespace GoogleAwarenessApi.Scripts.Internal
{
	public static class AwarenessUtils
	{
		public static string CommaJoin<T>(this IEnumerable<T> items)
		{
			return string.Join(",", items.Select(x => x.ToString()).ToArray());
		}

		public const string AwarenessClass = "com.google.android.gms.awareness.Awareness";
	}
}