using System;
using System.Collections;
using System.Collections.Generic;
using DeadMosquito.GoogleMapsView.Internal;
using GoogleAwarenessApi.Scripts.Internal;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi.Internal;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Main class to interact with snapshot API
	///
	/// See https://developers.google.com/android/reference/com/google/android/gms/awareness/SnapshotClient
	/// </summary>
	[PublicAPI]
	public static class SnapshotClient
	{
		const string SnapshotClientClass = "com.google.android.gms.awareness.SnapshotClient";

		static AndroidJavaObject _client;

		/// <summary>
		/// Gets the current information about nearby beacons. Note that beacon snapshots are only available on API level 18 or higher.
		///
		/// Gets the current information about nearby beacons. Note that beacon snapshots are only available on devices running API level 18 or higher.
		/// If calling from a device running API level 17 or earlier, the Task will fail and calling getStatusCode() will return status code API_NOT_AVAILABLE.
		/// </summary>
		/// <param name="beaconTypes">The types of beacon attachments to return. See https://developers.google.com/beacons/ for details about beacon attachments.</param>
		/// <param name="onSuccess">Success callback.</param>
		/// <param name="onFailure">Failure callback.</param>
		public static void GetBeaconState([NotNull] List<BeaconState.TypeFilter> beaconTypes, [NotNull] Action<BeaconState> onSuccess, [CanBeNull] Action<string> onFailure)
		{
			if (beaconTypes == null)
			{
				throw new ArgumentNullException("beaconTypes");
			}

			if (onSuccess == null)
			{
				throw new ArgumentNullException("onSuccess");
			}

			if (beaconTypes.Count == 0)
			{
				throw new ArgumentException("beaconTypes must not be empty");
			}

			if (CheckPreconditions())
			{
				return;
			}

			if (PermissionHelper.CheckLocationPermission())
			{
				return;
			}

			CreateClientLazy();

			var onSuccessListenerProxy = new OnSuccessListenerProxy<BeaconState>(onSuccess, ajo => BeaconState.FromAJO(ajo.CallAJO("getBeaconState")));
			_client.CallAJO("getBeaconState", beaconTypes.ToJavaList(x => x.AJO))
				.CallAJO("addOnSuccessListener", onSuccessListenerProxy)
				.CallAJO("addOnFailureListener", new OnFailureListenerProxy(onFailure));
		}

		/// <summary>
		/// Gets the user's current activity (e.g., running, walking, biking, driving, etc.).
		///
		/// To use this method, your app must declare the com.google.android.gms.permission.ACTIVITY_RECOGNITION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="onSuccess">Success callback.</param>
		/// <param name="onFailure">Failure callback.</param>
		public static void GetDetectedActivity([NotNull] Action<ActivityRecognitionResult> onSuccess, [CanBeNull] Action<string> onFailure)
		{
			if (onSuccess == null)
			{
				throw new ArgumentNullException("onSuccess");
			}

			if (CheckPreconditions())
			{
				return;
			}

			_client.CallAJO("getDetectedActivity")
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy<ActivityRecognitionResult>(onSuccess,
					ajo => ActivityRecognitionResult.FromAJO(ajo.CallAJO("getActivityRecognitionResult"))))
				.CallAJO("addOnFailureListener", new OnFailureListenerProxy(onFailure));
		}

		/// <summary>
		/// Reports whether headphones are plugged into the device.
		/// </summary>
		/// <param name="onSuccess">Invoked with result if success</param>
		/// <param name="onFailure">Invoked with error message if failed</param>
		public static void GetHeadphoneState([NotNull] Action<HeadphoneState> onSuccess, [CanBeNull] Action<string> onFailure)
		{
			if (onSuccess == null)
			{
				throw new ArgumentNullException("onSuccess");
			}

			if (CheckPreconditions())
			{
				return;
			}

			_client.CallAJO("getHeadphoneState")
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy<HeadphoneState>(onSuccess, ajo => (HeadphoneState) ajo.CallAJO("getHeadphoneState").CallInt("getState")))
				.CallAJO("addOnFailureListener", new OnFailureListenerProxy(onFailure));
		}

		/// <summary>
		/// Gets the device's current location (lat/lng).
		///
		/// To use this method, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="onSuccess">Success callback.</param>
		/// <param name="onFailure">Failure callback.</param>
		public static void GetLocation([NotNull] Action<Location> onSuccess, [CanBeNull] Action<string> onFailure)
		{
			if (onSuccess == null)
			{
				throw new ArgumentNullException("onSuccess");
			}

			if (CheckPreconditions())
			{
				return;
			}

			if (PermissionHelper.CheckLocationPermission())
			{
				return;
			}

			_client.CallAJO("getLocation")
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy<Location>(onSuccess, ajo => Location.FromAJO(ajo.CallAJO("getLocation"))))
				.CallAJO("addOnFailureListener", new OnFailureListenerProxy(onFailure));
		}

		/// <summary>
		/// Gets the device's current semantic location, or "place", which can include a name, place type, and address.
		///
		///To use this method, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="onSuccess">Success callback.</param>
		/// <param name="onFailure">Failure callback.</param>
		public static void GetPlaces([NotNull] Action<List<PlaceLikelihood>> onSuccess, [CanBeNull] Action<string> onFailure)
		{
			if (onSuccess == null)
			{
				throw new ArgumentNullException("onSuccess");
			}

			if (CheckPreconditions())
			{
				return;
			}

			if (PermissionHelper.CheckLocationPermission())
			{
				return;
			}

			_client.CallAJO("getPlaces")
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy<List<PlaceLikelihood>>(onSuccess,
					ajo => ajo.CallAJO("getPlaceLikelihoods").FromJavaList(PlaceLikelihood.FromAJO)))
				.CallAJO("addOnFailureListener", new OnFailureListenerProxy(onFailure));
		}

		/// <summary>
		/// Gets the semantic time intervals for the to the current time and location.
		///
		/// To use this method, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="onSuccess">Success callback.</param>
		/// <param name="onFailure">Failure callback.</param>
		public static void GetTimeIntervals([NotNull] Action<TimeIntervals> onSuccess, [CanBeNull] Action<string> onFailure)
		{
			if (onSuccess == null)
			{
				throw new ArgumentNullException("onSuccess");
			}

			if (CheckPreconditions())
			{
				return;
			}

			_client.CallAJO("getTimeIntervals")
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy<TimeIntervals>(onSuccess, ajo =>
				{
					var intervals = ajo.CallAJO("getTimeIntervals").Call<int[]>("getTimeIntervals");
					return new TimeIntervals(Array.ConvertAll(intervals, i => (TimeInterval) i));
				}))
				.CallAJO("addOnFailureListener", new OnFailureListenerProxy(onFailure));
		}

		/// <summary>
		/// Gets the current weather conditions (temperature, feels-like temperature, dewpoint, humidity, etc.) at the current device location.
		///
		/// To use this method, your app must declare the android.permission.ACCESS_FINE_LOCATION permission in AndroidManifest.xml, and the user must provide consent at runtime.
		/// </summary>
		/// <param name="onSuccess">Success callback.</param>
		/// <param name="onFailure">Failure callback.</param>
		public static void GetWeather([NotNull] Action<Weather> onSuccess, [CanBeNull] Action<string> onFailure)
		{
			if (onSuccess == null)
			{
				throw new ArgumentNullException("onSuccess");
			}

			if (CheckPreconditions())
			{
				return;
			}

			if (PermissionHelper.CheckLocationPermission())
			{
				return;
			}

			_client.CallAJO("getWeather")
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy<Weather>(onSuccess, ajo =>
				{
					var weatherAJO = ajo.CallAJO("getWeather");
					return weatherAJO.IsJavaNull() ? null : new Weather(weatherAJO);
				}))
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

		static void CreateClientLazy()
		{
			if (_client != null)
			{
				return;
			}

			_client = AwarenessUtils.AwarenessClass.AJCCallStaticOnceAJO("getSnapshotClient", JniToolkitUtils.Activity);
			AwarenessSceneHelper.Init();
		}
	}
}