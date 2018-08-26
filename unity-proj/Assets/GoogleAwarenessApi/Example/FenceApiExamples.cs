using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;
using UnityEngine;
using UnityEngine.UI;

public class FenceApiExamples : MonoBehaviour
{
	[SerializeField]
	Text text;

	const string ExecrcisingWithHeadphonesKey = "fence_key";
	const string AllHeadphonesKey = "headphones_fence_key";
	const string AllLocationKey = "location_fence_key";

	#region fences_api

	[UsedImplicitly]
	public void OnQueryFences()
	{
		var allFences = FenceQueryRequest.All();
		var requst = FenceQueryRequest.ForFences(AllHeadphonesKey, AllLocationKey);
		FenceClient.QueryFences(requst, response => { }, error => { });
	}

	[UsedImplicitly]
	public void OnSetupComplexFence()
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
		var notWalkingWithHeadphones = AwarenessFence.And(AwarenessFence.Not(walkingFence), headphoneFence);

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
	public void OnSetupHeadphonesFence()
	{
		var fence = AwarenessFence.Or(HeadphoneFence.During(HeadphoneState.PluggedIn), HeadphoneFence.PluggingIn(), HeadphoneFence.Unplugging());
		FenceClient.UpdateFences(new FenceUpdateRequest.Builder()
			.AddFence(AllHeadphonesKey, fence)
			.Build(), OnUpdateFencesSuccess, OnUpdateFencesFailure);
	}

	[UsedImplicitly]
	public void OnSetupLocationFence()
	{
		var fence = AwarenessFence.Or(
			LocationFence.Entering(0, 0, 1000),
			LocationFence.Exiting(0, 0, 1000),
			LocationFence.In(0, 0, 1000, 100)
		);
		FenceClient.UpdateFences(new FenceUpdateRequest.Builder()
			.AddFence(AllLocationKey, fence)
			.Build(), OnUpdateFencesSuccess, OnUpdateFencesFailure);
	}

	[UsedImplicitly]
	public void OnSetupTimeFence()
	{
		const long hourBefore = -1L * 60L * 60L * 1000L;
		const long hourAfter = 1L * 60L * 60L * 1000L;

		var aroundSunriseOrSunset = AwarenessFence.Or(
			TimeFence.AroundTimeInstant(TimeFence.TimeInstant.Sunrise, hourBefore, hourAfter),
			TimeFence.AroundTimeInstant(TimeFence.TimeInstant.Sunset, hourBefore, hourAfter)
		);
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