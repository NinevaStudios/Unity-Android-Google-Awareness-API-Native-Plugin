using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public static class SnapshotClient
	{
		static AndroidJavaObject _client;

		public static void GetBeaconState(Action<BeaconStateResponse> onSuccess)
		{
			CreateClientLazy();
		}

		public static void GetDetectedActivity(Action<DetectedActivityResponse> onSuccess)
		{
			CreateClientLazy();
		}

		public static void GetHeadphoneState(Action<HeadphoneStateResponse> onSuccess)
		{
			CreateClientLazy();
		}

		public static void GetLocation(Action<LocationResponse> onSuccess)
		{
			CreateClientLazy();
		}

		public static void GetPlaces(Action<PlacesResponse> onSuccess)
		{
			CreateClientLazy();
		}

		public static void GetTimeIntervals(Action<TimeIntervalsResponse> onSuccess)
		{
			CreateClientLazy();
		}

		public static void GetWeather(Action<WeatherResponse> onSuccess)
		{
			CreateClientLazy();
		}

		static void CreateClientLazy()
		{
			if (_client == null)
			{
			}
		}
	}
}