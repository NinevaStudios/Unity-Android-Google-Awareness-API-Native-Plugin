using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Use this class to create beacon fences.
	///
	/// The fences in this class support detecting nearby beacons that are associated with attachments, which are a triple of namespace, type, and content.
	///
	/// Note: Values that indicate a changing state are momentarily <see cref="FenceState.State.True"/> for about 5 seconds, then automatically revert to <see cref="FenceState.State.False"/>.
	/// </summary>
	[PublicAPI]
	public class BeaconFence
	{
		const string BeaconFenceClass = "com.google.android.gms.awareness.fence.BeaconFence";

		/// <summary>
		/// This fence is momentarily <see cref="FenceState.State.True"/> (about 5 seconds) when a beacon with the specified types is found.
		///
		/// To use this API, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="beaconTypes"><see cref="BeaconState.TypeFilter"/> non-empty beacon types listing to scan for.</param>
		/// <returns><see cref="AwarenessFence"/></returns>
		public static AwarenessFence Found(IEnumerable<BeaconState.TypeFilter> beaconTypes)
		{
			if (PermissionHelper.CheckLocationPermission())
			{
				throw new InvalidOperationException();
			}
			
			var filters = beaconTypes.ToList().ToJavaList(x => x.AJO);
			return new AwarenessFence(BeaconFenceClass.AJCCallStaticOnceAJO("found", filters));
		}

		/// <summary>
		/// This fence is momentarily <see cref="FenceState.State.True"/> (about 5 seconds) when a beacon with the specified types is found.
		///
		/// To use this API, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="beaconTypes"><see cref="BeaconState.TypeFilter"/> non-empty beacon types listing to scan for.</param>
		/// <returns><see cref="AwarenessFence"/></returns>
		public static AwarenessFence Found(params BeaconState.TypeFilter[] beaconTypes)
		{
			return Found(beaconTypes.ToList());
		}

		/// <summary>
		/// This fence is momentarily <see cref="FenceState.State.True"/> (about 5 seconds) when a beacon with the specified types is lost.
		///
		/// To use this API, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="beaconTypes"><see cref="BeaconState.TypeFilter"/> non-empty beacon types listing to scan for.</param>
		/// <returns><see cref="AwarenessFence"/></returns>
		public static AwarenessFence Lost(IEnumerable<BeaconState.TypeFilter> beaconTypes)
		{
			if (PermissionHelper.CheckLocationPermission())
			{
				throw new InvalidOperationException();
			}
			
			var filters = beaconTypes.ToList().ToJavaList(x => x.AJO);
			return new AwarenessFence(BeaconFenceClass.AJCCallStaticOnceAJO("lost", filters));
		}

		/// <summary>
		/// This fence is momentarily <see cref="FenceState.State.True"/> (about 5 seconds) when a beacon with the specified types is lost.
		///
		/// To use this API, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="beaconTypes"><see cref="BeaconState.TypeFilter"/> non-empty beacon types listing to scan for.</param>
		/// <returns><see cref="AwarenessFence"/></returns>
		public static AwarenessFence Lost(params BeaconState.TypeFilter[] beaconTypes)
		{
			return Lost(beaconTypes.ToList());
		}

		/// <summary>
		/// This fence is momentarily <see cref="FenceState.State.True"/> (about 5 seconds) when a beacon with the specified types is found but not lost.
		///
		/// To use this API, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="beaconTypes"><see cref="BeaconState.TypeFilter"/> non-empty beacon types listing to scan for.</param>
		/// <returns><see cref="AwarenessFence"/></returns>
		public static AwarenessFence Near(IEnumerable<BeaconState.TypeFilter> beaconTypes)
		{
			if (PermissionHelper.CheckLocationPermission())
			{
				throw new InvalidOperationException();
			}
			
			var filters = beaconTypes.ToList().ToJavaList(x => x.AJO);
			return new AwarenessFence(BeaconFenceClass.AJCCallStaticOnceAJO("near", filters));
		}

		/// <summary>
		/// This fence is momentarily <see cref="FenceState.State.True"/> (about 5 seconds) when a beacon with the specified types is found but not lost.
		///
		/// To use this API, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="beaconTypes"><see cref="BeaconState.TypeFilter"/> non-empty beacon types listing to scan for.</param>
		/// <returns><see cref="AwarenessFence"/></returns>
		public static AwarenessFence Near(params BeaconState.TypeFilter[] beaconTypes)
		{
			return Near(beaconTypes.ToList());
		}
	}
}