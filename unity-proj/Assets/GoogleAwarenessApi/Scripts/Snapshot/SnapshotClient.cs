using System;
using System.Collections;
using System.Collections.Generic;
using DeadMosquito.GoogleMapsView.Internal;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi.Internal;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public static class SnapshotClient
	{
		const string SnapshotClientClass = "com.google.android.gms.awareness.SnapshotClient";
		const string AwarenessClass = "com.google.android.gms.awareness.Awareness";
		
		static AndroidJavaObject _client;

		public static void GetBeaconState(Action<BeaconStateResponse> onSuccess)
		{
			CreateClientLazy();
		}

		public static void GetDetectedActivity(Action<DetectedActivityResponse> onSuccess)
		{
			CreateClientLazy();
		}

		public static void GetHeadphoneState(Action<HeadphoneState> onSuccess, Action<string> onFailure)
		{
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return;
			}
			
			CreateClientLazy();

			_client.CallAJO("getHeadphoneState")
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy(onSuccess))
				.CallAJO("addOnFailureListener", new OnFailureListenerProxy(onFailure));
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
			if (_client != null)
			{
				return;
			}

			_client = AwarenessClass.AJCCallStaticOnceAJO("getSnapshotClient", JniToolkitUtils.Activity);
			AwarenessSceneHelper.Init();
		}
	}
}