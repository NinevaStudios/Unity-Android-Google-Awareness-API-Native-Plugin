using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	///     A combination of conditions on two or more types of context, which trigger a callback to the app when satisfied.
	/// </summary>
	[PublicAPI]
	public class AwarenessFence : IDisposable
	{
		public const string AwarenessFenceClass = "com.google.android.gms.awareness.fence.AwarenessFence";

		public AndroidJavaObject AJO { get; private set; }

		internal AwarenessFence(AndroidJavaObject ajo)
		{
			AJO = ajo;
		}

		public void Dispose()
		{
			if (AJO != null)
			{
				AJO.Dispose();
			}
		}

		/// <summary>
		///     Create an awareness fence that is the logical AND of the specified fences.
		/// </summary>
		/// <param name="fences">Collection of fences that should be combined with AND.</param>
		/// <returns>Collection of fences that should be combined with AND.</returns>
		public static AwarenessFence And(params AwarenessFence[] fences)
		{
			var androidJavaObject = fences.ToList().ToJavaList(x => x.AJO);
			return new AwarenessFence(AwarenessFenceClass.AJCCallStaticOnceAJO("and", androidJavaObject));
		}

		/// <summary>
		///     Create an awareness fence that is the logical AND of fences in the specified fence collection.
		/// </summary>
		/// <param name="fences">Collection of fences that should be combined with AND.</param>
		/// <returns>Collection of fences that should be combined with AND.</returns>
		public static AwarenessFence And(IEnumerable<AwarenessFence> fences)
		{
			return And(fences.ToArray());
		}

		/// <summary>
		///     Create an awareness fence that is the logical NOT of the specified fence.
		/// </summary>
		/// <param name="fence">The fence that should be passed through a logical NOT.</param>
		/// <returns>The resulting awareness fence.</returns>
		public static AwarenessFence Not(AwarenessFence fence)
		{
			return new AwarenessFence(AwarenessFenceClass.AJCCallStaticOnceAJO("not", fence.AJO));
		}

		/// <summary>
		///     Create an awareness fence that is the logical OR of the specified fences.
		/// </summary>
		/// <param name="fences">Collection of fences that should be combined with OR.</param>
		/// <returns>The resulting combined awareness fence.</returns>
		public static AwarenessFence Or(params AwarenessFence[] fences)
		{
			var androidJavaObject = fences.ToList().ToJavaList(x => x.AJO);
			return new AwarenessFence(AwarenessFenceClass.AJCCallStaticOnceAJO("or", androidJavaObject));
		}

		/// <summary>
		///     Create an awareness fence that is the logical OR of the specified fences.
		/// </summary>
		/// <param name="fences">Collection of fences that should be combined with OR.</param>
		/// <returns>The resulting combined awareness fence.</returns>
		public static AwarenessFence Or(IEnumerable<AwarenessFence> fences)
		{
			return Or(fences.ToArray());
		}
	}
}