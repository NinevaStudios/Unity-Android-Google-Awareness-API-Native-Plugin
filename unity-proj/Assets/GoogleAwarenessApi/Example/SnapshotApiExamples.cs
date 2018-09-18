using System.Collections.Generic;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;
using UnityEngine;
using UnityEngine.UI;

namespace GoogleAwarenessApi.Example
{
	public class SnapshotApiExamples : MonoBehaviour
	{
		[SerializeField]
		Text text;

		[UsedImplicitly]
		public void OnGetDetectedActivity()
		{
			SnapshotClient.GetDetectedActivity(result =>
			{
				Debug.Log("Still confidence: " + result.GetActivityConfidence(DetectedActivity.ActivityType.Still));
				Debug.Log("Running confidence: " + result.GetActivityConfidence(DetectedActivity.ActivityType.Running));
				LogSuccess(result);
			}, LogFailure);
		}

		[UsedImplicitly]
		public void OnRequestLocationPermission()
		{
			if (PermissionHelper.LocationPermissionGranted)
			{
				Debug.Log("Location permission is already granted");
				return;
			}
			
			PermissionHelper.RequestLocationPermission(LogSuccess);
		}

		[UsedImplicitly]
		public void OnGetHeadphonesState()
		{
			SnapshotClient.GetHeadphoneState(state => LogSuccess(state), LogFailure);
		}

		[UsedImplicitly]
		public void OnGetWeather()
		{
			SnapshotClient.GetWeather(LogSuccess, LogFailure);
		}

		[UsedImplicitly]
		public void OnGetLocation()
		{
			SnapshotClient.GetLocation(location => LogSuccess(location), LogFailure);
		}

		[UsedImplicitly]
		public void OnGetTimeIntervals()
		{
			SnapshotClient.GetTimeIntervals(LogSuccess, LogFailure);
		}

		[UsedImplicitly]
		public void OnGetNearbyPlaces()
		{
			SnapshotClient.GetPlaces(places => places.ForEach(LogSuccess), LogFailure);
		}

		[UsedImplicitly]
		public void OnGetBeaconState()
		{
			// Configure your beacons first! https://developers.google.com/beacons/get-started#2-configure-your-beacons
			
			var theNamespace = "awareness-api-1534415879510";
			var beaconTypes = new List<BeaconState.TypeFilter>
			{
				// must be registered on google dashboard https://developers.google.com/beacons/dashboard/
				BeaconState.TypeFilter.With(theNamespace, "string"), 
				BeaconState.TypeFilter.With("com.google.nearby", "en"), 
				BeaconState.TypeFilter.With(theNamespace, "x")
			};
			SnapshotClient.GetBeaconState(beaconTypes, state =>
			{
				if (state.BeaconInfos.Count == 0)
				{
					LogSuccess("No beacons found");
				}
				else
				{
					LogSuccess(state);
				}
			}, LogFailure);
		}

		void LogFailure(string err)
		{
			text.text = err;
			Debug.LogError(err);
		}

		void LogSuccess(object result)
		{
			if (result != null)
			{
				text.text = result.ToString();
				Debug.Log(result);
			}
			else
			{
				Debug.LogWarning("Result is null");
			}
		}
	}
}