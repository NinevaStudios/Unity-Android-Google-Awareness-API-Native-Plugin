using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// An immutable class representing a latitude/longitude aligned rectangle.
	/// </summary>
	[PublicAPI]
	public sealed class LatLngBounds
	{
		public static readonly LatLngBounds Zero = new LatLngBounds(LatLng.Zero, LatLng.Zero);
		
		readonly LatLng _southwest;
		readonly LatLng _northeast;

		/// <summary>
		/// Creates a new bounds based on a southwest and a northeast corner.
		/// </summary>
		/// <param name="southwest">Southwest corner.</param>
		/// <param name="northeast">Northeast corner.</param>
		public LatLngBounds(LatLng southwest, LatLng northeast)
		{
			_southwest = southwest;
			_northeast = northeast;
		}

		public AndroidJavaObject ToAJO()
		{
			if (JniToolkitUtils.IsAndroidRuntime)
			{
				return null;
			}

			return new AndroidJavaObject("com.google.android.gms.maps.model.LatLngBounds", _southwest.ToAJO(),
				_northeast.ToAJO());
		}

		public static LatLngBounds FromAJO(AndroidJavaObject ajo)
		{
			if (JniToolkitUtils.IsAndroidRuntime)
			{
				return new LatLngBounds(LatLng.Zero, LatLng.Zero);
			}

			var northeast = LatLng.FromAJO(ajo.GetAJO("northeast"));
			var southwest = LatLng.FromAJO(ajo.GetAJO("southwest"));
			return new LatLngBounds(southwest, northeast);
		}

		public override string ToString()
		{
			return string.Format("[LatLngBounds SW: {0}, NE: {1}]", _southwest, _northeast);
		}
	}
}