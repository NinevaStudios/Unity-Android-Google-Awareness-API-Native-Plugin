using System;
using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Use this class to create location-based fences.
	///
	/// Note: Values that indicate a changing state are momentarily <see cref="FenceState.State.True"/> for about 5 seconds, then automatically revert to <see cref="FenceState.State.False"/>.
	/// </summary>
	[PublicAPI]
	public class LocationFence
	{
		const string LocationFenceClass = "com.google.android.gms.awareness.fence.LocationFence";

		/// <summary>
		/// This fence is momentarily (about 5 seconds) in the <see cref="FenceState.State.True"/> state when the user enters the specified circle.
		///
		/// To use this method, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="latitude">Center latitude of the circle in degrees, between -90 and +90 inclusive.</param>
		/// <param name="longitude">Center longitude of the circle in degrees, between -180 and +180 inclusive.</param>
		/// <param name="radius">Radius defining the circular region, in meters.</param>
		/// <returns>the <see cref="AwarenessFence"/> object representing this fence.</returns>
		public static AwarenessFence Entering(double latitude, double longitude, double radius)
		{
			if (PermissionHelper.CheckLocationPermission())
			{
				throw new InvalidOperationException();
			}
			
			return new AwarenessFence(LocationFenceClass.AJCCallStaticOnceAJO("entering", longitude, latitude, radius));
		}

		/// <summary>
		/// This fence is momentarily (about 5 seconds) in the <see cref="FenceState.State.True"/> state when the user exits the specified circle.
		///
		/// Your app must declare the android.permission.ACCESS_FINE_LOCATION permission in your AndroidManifest and be granted this permission to use this API.
		/// </summary>
		/// <param name="latitude">Center latitude of the circle in degrees, between -90 and +90 inclusive.</param>
		/// <param name="longitude">Center longitude of the circle in degrees, between -180 and +180 inclusive.</param>
		/// <param name="radius">Radius defining the circular region, in meters.</param>
		/// <returns>the <see cref="AwarenessFence"/> object representing this fence.</returns>
		public static AwarenessFence Exiting(double latitude, double longitude, double radius)
		{
			if (PermissionHelper.CheckLocationPermission())
			{
				throw new InvalidOperationException();
			}
			
			return new AwarenessFence(LocationFenceClass.AJCCallStaticOnceAJO("exiting", longitude, latitude, radius));
		}

		/// <summary>
		/// This fence is in the <see cref="FenceState.State.True"/> state when the user's location is within the specified circle, and the user has been in the circle for at least the <code>dwellTimeMillis</code> that was specified.
		///
		/// To use this method, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="latitude">Center latitude of the circle in degrees, between -90 and +90 inclusive.</param>
		/// <param name="longitude">Center longitude of the circle in degrees, between -180 and +180 inclusive.</param>
		/// <param name="radius">Radius defining the circular region, in meters.</param>
		/// <param name="dwellTimeMillis">Minimum dwelling time inside a location before the fence is in the <see cref="FenceState.State.True"/> state. Must be a value greater than or equal to 0L.</param>
		/// <returns>the <see cref="AwarenessFence"/> object representing this fence.</returns>
		public static AwarenessFence In(double latitude, double longitude, double radius, long dwellTimeMillis)
		{
			if (PermissionHelper.CheckLocationPermission())
			{
				throw new InvalidOperationException();
			}
			
			return new AwarenessFence(LocationFenceClass.AJCCallStaticOnceAJO("in", longitude, latitude, radius, dwellTimeMillis));
		}
	}
}