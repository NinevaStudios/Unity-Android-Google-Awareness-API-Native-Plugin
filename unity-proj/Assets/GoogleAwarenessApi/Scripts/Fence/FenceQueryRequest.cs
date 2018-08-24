using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Defines the interface for querying fences in the <see cref="FenceClient"/>.
	/// </summary>
	[PublicAPI]
	public class FenceQueryRequest
	{
		const string FenceQueryRequestClass = "com.google.android.gms.awareness.fence.FenceQueryRequest";

		public AndroidJavaObject AJO { get; private set; }

		FenceQueryRequest(AndroidJavaObject ajo)
		{
			AJO = ajo;
		}
		
		/// <summary>
		/// Query all the fences from the <see cref="FenceClient"/> instance corresponding to the calling package.
		/// </summary>
		/// <returns><see cref="FenceQueryRequest"/></returns>
		public static FenceQueryRequest All()
		{
			return new FenceQueryRequest(FenceQueryRequestClass.AJCCallStaticOnceAJO("all"));
		}
		
		/// <summary>
		/// Query the defined fences for the given keys.
		/// </summary>
		/// <param name="fenceKeys">Fence keys for querying fences in the <see cref="FenceClient"/>. The fence keys should not be null.</param>
		/// <returns><see cref="FenceQueryRequest"/></returns>
		public static FenceQueryRequest ForFences(params string[] fenceKeys)
		{
			var androidJavaObject = fenceKeys.ToList().ToJavaList(x => x);
			return new FenceQueryRequest(FenceQueryRequestClass.AJCCallStaticOnceAJO("forFences", androidJavaObject));
		}
		
		/// <summary>
		/// Query the defined fences for the given keys.
		/// </summary>
		/// <param name="fenceKeys">Fence keys for querying fences in the <see cref="FenceClient"/>. The fence keys should not be null.</param>
		/// <returns><see cref="FenceQueryRequest"/></returns>
		public static FenceQueryRequest ForFences(IEnumerable<string> fenceKeys)
		{
			return ForFences(fenceKeys.ToArray());
		}
	}
}