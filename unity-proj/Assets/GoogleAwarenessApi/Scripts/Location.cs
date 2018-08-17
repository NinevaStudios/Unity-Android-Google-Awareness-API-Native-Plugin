using System;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public struct Location
	{
		readonly double _latitude;
		readonly double _longitude;
		readonly bool _hasAccuracy;
		readonly float _accuracy;
		readonly long _timestamp;

		/// <summary>
		/// Gets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		[PublicAPI]
		public double Latitude
		{
			get { return _latitude; }
		}

		/// <summary>
		/// Gets the Longitude.
		/// </summary>
		/// <value>The Longitude.</value>
		[PublicAPI]
		public double Longitude
		{
			get { return _longitude; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance has accuracy.
		/// </summary>
		/// <value><c>true</c> if this instance has accuracy; otherwise, <c>false</c>.</value>
		[PublicAPI]
		public bool HasAccuracy
		{
			get { return _hasAccuracy; }
		}

		/// <summary>
		/// Gets the location accuracy.
		/// </summary>
		/// <value>The location accuracy.</value>
		[PublicAPI]
		public float Accuracy
		{
			get { return _accuracy; }
		}

		/// <summary>
		/// True if this location has a speed.
		/// </summary>
		[PublicAPI]
		public bool HasSpeed { get; set; }

		/// <summary>
		/// Get the speed if it is available, in meters/second over ground.
		/// 
		/// If this location does not have a speed then 0.0 is returned.
		/// </summary>
		[PublicAPI]
		public float Speed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[PublicAPI]
		public bool HasBearing { get; set; }

		/// <summary>
		/// Get the bearing, in degrees.
		/// Bearing is the horizontal direction of travel of this device, and is not related to the device orientation.It is guaranteed to be in the range (0.0, 360.0] if the device has a bearing.
		/// If this location does not have a bearing then 0.0 is returned.
		/// </summary>
		[PublicAPI]
		public float Bearing { get; set; }

		/// <summary>
		/// WARNING! This API was added in API level 18. It will always return false if Android version is below 18;
		///
		/// Returns true if the Location came from a mock provider.
		/// </summary>
		[PublicAPI]
		public bool IsFromMockProvider { get; set; }


		/// <summary>
		/// Return the UTC time of this fix, in milliseconds since January 1, 1970.
		/// </summary>
		[PublicAPI]
		public long Timestamp
		{
			get { return _timestamp; }
		}

		[PublicAPI]
		public Location(double latitude, double longitude, bool hasAccuracy, float accuracy, long timestamp) : this()
		{
			_latitude = latitude;
			_longitude = longitude;
			_hasAccuracy = hasAccuracy;
			_accuracy = accuracy;
			_timestamp = timestamp;
			Speed = 0f;
			Bearing = 0f;
		}

		public static Location FromAJO( /*Location*/ AndroidJavaObject locationAJO)
		{
			if (locationAJO.IsJavaNull())
			{
				return new Location();
			}

			using (locationAJO)
			{
				var latitude = locationAJO.Call<double>("getLatitude");
				var longitude = locationAJO.Call<double>("getLongitude");
				var hasAccuracy = locationAJO.Call<bool>("hasAccuracy");
				var accuracy = locationAJO.Call<float>("getAccuracy");
				long time = locationAJO.Call<long>("getTime");

				var hasSpeed = locationAJO.CallBool("hasSpeed");
				var speed = locationAJO.Call<float>("getSpeed");
				var hasBearing = locationAJO.CallBool("hasBearing");
				var bearing = locationAJO.Call<float>("getBearing");


				var result = new Location(latitude, longitude, hasAccuracy, accuracy, time);

				if (hasSpeed)
				{
					result.HasSpeed = true;
					result.Speed = speed;
				}

				if (hasBearing)
				{
					result.HasBearing = true;
					result.Bearing = bearing;
				}

				bool isFromMockProvider = false;
				try
				{
					isFromMockProvider = locationAJO.CallBool("isFromMockProvider");
				}
				catch (Exception)
				{
					// Ignore
				}

				result.IsFromMockProvider = isFromMockProvider;

				return result;
			}
		}

		public override string ToString()
		{
			return string.Format("[Location: Latitude={0}, Longitude={1}, " +
			                     "HasAccuracy={2}, Accuracy={3}, " +
			                     "Timestamp={4}, " +
			                     "HasSpeed={5}, Speed={6}, " +
			                     "HasBearing={7}, Bearing={8}, " +
			                     "IsFromMockProvider={9}]",
				Latitude, Longitude,
				HasAccuracy, Accuracy,
				Timestamp,
				HasSpeed, Speed,
				HasBearing, Bearing,
				IsFromMockProvider);
		}
	}
}