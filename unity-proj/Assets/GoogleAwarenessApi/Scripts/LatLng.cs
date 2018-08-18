using System;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// https://developers.google.com/android/reference/com/google/android/gms/maps/model/LatLng
	/// 
	/// An immutable class representing a pair of latitude and longitude coordinates, stored as degrees.
	/// </summary>
	[PublicAPI]
	public sealed class LatLng
	{
		public static readonly LatLng Zero = new LatLng(0, 0);

		readonly double _latitude;
		readonly double _longitude;

		/// <summary>
		/// Latitude
		/// </summary>
		[PublicAPI]
		public double Latitude
		{
			get { return _latitude; }
		}

		/// <summary>
		/// Longitude
		/// </summary>
		[PublicAPI]
		public double Longitude
		{
			get { return _longitude; }
		}

		/// <summary>
		/// Constructs a <see cref="LatLng"/> with the other <see cref="LatLng"/> values
		/// </summary>
		/// <param name="latLng"><see cref="LatLng"/> to make a copy of</param>
		[PublicAPI]
		public LatLng(LatLng latLng)
		{
			_latitude = latLng._latitude;
			_longitude = latLng._longitude;
		}

		/// <summary>
		/// Constructs a <see cref="LatLng"/> with the given latitude and longitude, measured in degrees.
		/// </summary>
		/// <param name="latitude">Latitude.</param>
		/// <param name="longitude">Longitude.</param>
		[PublicAPI]
		public LatLng(double latitude, double longitude)
		{
			_latitude = Math.Max(-90.0D, Math.Min(90.0D, latitude));

			if (-180.0D <= longitude && longitude < 180.0D)
			{
				_longitude = longitude;
			}
			else
			{
				_longitude = ((longitude - 180.0D) % 360.0D + 360.0D) % 360.0D - 180.0D;
			}
		}

		public override string ToString()
		{
			return new StringBuilder(60).Append("lat/lng: (").Append(_latitude).Append(",").Append(_longitude).Append(")")
				.ToString();
		}

		[NotNull]
		public static LatLng FromAJO(AndroidJavaObject ajo)
		{
			return JniToolkitUtils.IsAndroidRuntime
				? new LatLng(ajo.GetDouble("latitude"), ajo.GetDouble("longitude"))
				: new LatLng(0, 0);
		}

		public AndroidJavaObject ToAJO()
		{
			return JniToolkitUtils.IsAndroidRuntime
				? new AndroidJavaObject("com.google.android.gms.maps.model.LatLng", _latitude, _longitude)
				: null;
		}
	}
}