using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public class BeaconState
	{
		const string TypeFilterClass = "com.google.android.gms.awareness.state.BeaconState$TypeFilter";
		
		/// <summary>
		/// The type of beacon to match. Beacons can be specified by either:
		/// 	- A String match on both the namespace and type associated with the beacon.
		///		- A String match on the namespace, type, and byte-for-byte match on the content.
		/// </summary>
		[PublicAPI]
		public class TypeFilter
		{
			public AndroidJavaObject AJO { get; }

			TypeFilter(AndroidJavaObject ajo)
			{
				AJO = ajo;
			}

			/// <summary>
			/// Creates a <see cref="BeaconState.TypeFilter"/> that matches against beacons with the given <see cref="theNamespace"/> and <see cref="type"/>, regardless of the content.
			/// </summary>
			/// <param name="theNamespace">Beacon namespace to match against.</param>
			/// <param name="type">beacon Type to match against.</param>
			/// <returns><see cref="BeaconState.TypeFilter"/></returns>
			public static TypeFilter With(string theNamespace, string type)
			{
				return new TypeFilter(TypeFilterClass.AJCCallStaticOnceAJO("with", theNamespace, type));
			}
			
			/// <summary>
			/// Creates a <see cref="BeaconState.TypeFilter"/> that matches against beacons with the given <see cref="theNamespace"/> and <see cref="type"/>, and <see cref="content"/>.
			/// </summary>
			/// <param name="theNamespace">Beacon namespace to match against.</param>
			/// <param name="type">beacon Type to match against.</param>
			/// <param name="content">beacon content to match against.</param>
			/// <returns><see cref="BeaconState.TypeFilter"/></returns>
			public static TypeFilter With(string theNamespace, string type, byte[] content)
			{
				return new TypeFilter(TypeFilterClass.AJCCallStaticOnceAJO("with", theNamespace, type, content));
			}
		}
	}
}