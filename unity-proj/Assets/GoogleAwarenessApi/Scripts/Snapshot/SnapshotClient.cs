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
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return;
			}
			
			CreateClientLazy();
		}

		public static void GetDetectedActivity(Action<DetectedActivityResponse> onSuccess)
		{
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return;
			}
			
			CreateClientLazy();
		}

		/// <summary>
		/// Reports whether headphones are plugged into the device.
		/// </summary>
		/// <param name="onSuccess">Invoked with result if success</param>
		/// <param name="onFailure">Invoked with error message if failed</param>
		public static void GetHeadphoneState(Action<HeadphoneState> onSuccess, Action<string> onFailure)
		{
			if (CheckPreconditions())
			{
				return;
			}

			_client.CallAJO("getHeadphoneState")
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy<HeadphoneState>(onSuccess, ajo => (HeadphoneState) ajo.CallAJO("getHeadphoneState").CallInt("getState")))
				.CallAJO("addOnFailureListener", new OnFailureListenerProxy(onFailure));
		}

		static bool CheckPreconditions()
		{
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return true;
			}

			CreateClientLazy();
			return false;
		}

		public static void GetLocation(Action<LocationResponse> onSuccess)
		{
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return;
			}
			
			CreateClientLazy();
		}

		public static void GetPlaces(Action<PlacesResponse> onSuccess)
		{
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return;
			}
			
			CreateClientLazy();
		}

		public static void GetTimeIntervals(Action<TimeIntervals> onSuccess, Action<string> onFailure)
		{
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return;
			}
			
			CreateClientLazy();
		}

		public static void GetWeather(Action<WeatherResponse> onSuccess)
		{
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return;
			}
			
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