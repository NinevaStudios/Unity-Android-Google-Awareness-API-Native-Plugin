using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;
using UnityEngine;
using UnityEngine.UI;

namespace GoogleAwarenessApi.Example
{
	public class SnaphotApiExamples : MonoBehaviour
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
			SnapshotClient.GetBeaconState(LogSuccess, LogFailure);
		}

		void LogFailure(string err)
		{
			text.text = err;
			Debug.LogError(err);
		}

		void LogSuccess(object result)
		{
			text.text = result.ToString();
			Debug.Log(result);
		}
	}
}