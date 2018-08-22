using System;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;
using UnityEngine;
using UnityEngine.UI;

public class AwarenessExamples : MonoBehaviour
{
	[SerializeField]
	Text text;
	
	const string ExecrcisingWithHeadphonesKey = "fence_key";
	const string AllHeadphonesKey = "headphones_fence_key";
	
	#region snaphsot_API
	
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

	#endregion

	#region fences_api

	[UsedImplicitly]
	public void OnSetupFences()
	{
		// DetectedActivityFence will fire when it detects the user performing the specified
		// activity.  In this case it's walking.
		var walkingFence = DetectedActivityFence.During(DetectedActivityFence.ActivityType.Walking);
		
		// There are lots of cases where it's handy for the device to know if headphones have been
		// plugged in or unplugged.  For instance, if a music app detected your headphones fell out
		// when you were in a library, it'd be pretty considerate of the app to pause itself before
		// the user got in trouble.
		var headphoneFence = HeadphoneFence.During(HeadphoneState.PluggedIn);
		
		// Combines multiple fences into a compound fence.  While the first two fences trigger
		// individually, this fence will only trigger its callback when all of its member fences
		// hit a true state.
		var walkingWithHeadphones = AwarenessFence.And(walkingFence, headphoneFence);
		
		// We can even nest compound fences.  Using both "and" and "or" compound fences, this
		// compound fence will determine when the user has headphones in and is engaging in at least
		// one form of exercise.
		// The below breaks down to "(headphones plugged in) AND (walking OR running OR bicycling)"
		var exercisingWithHeadphonesFence = AwarenessFence.And(
			headphoneFence,
			AwarenessFence.Or(
				walkingFence,
				DetectedActivityFence.During(DetectedActivityFence.ActivityType.Running),
				DetectedActivityFence.During(DetectedActivityFence.ActivityType.OnBicycle)));
		
		// Now that we have an interesting, complex condition, register the fence to receive
		// callbacks.
		
		FenceClient.UpdateFences(new FenceUpdateRequest.Builder()
			.AddFence(ExecrcisingWithHeadphonesKey, exercisingWithHeadphonesFence)
			.Build(), OnUpdateFencesSuccess, OnUpdateFencesFailure);
	}
	
	[UsedImplicitly]
	public void OnSetupHeadphonesFence() {
		var fence = AwarenessFence.Or(HeadphoneFence.During(HeadphoneState.PluggedIn), HeadphoneFence.PluggingIn(), HeadphoneFence.Unplugging());
		FenceClient.UpdateFences(new FenceUpdateRequest.Builder()
			.AddFence(AllHeadphonesKey, fence)
			.Build(), OnUpdateFencesSuccess, OnUpdateFencesFailure);
	}

	void OnUpdateFencesFailure(string err)
	{
		LogFailure(err);
	}

	void OnUpdateFencesSuccess()
	{
		LogSuccess("Fences suggessfully updated");
	}

	#endregion

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
