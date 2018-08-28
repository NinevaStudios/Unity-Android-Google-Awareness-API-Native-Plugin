using System;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;
using UnityEngine;
using UnityEngine.UI;

public class FenceApiExamples : MonoBehaviour
{
	[SerializeField]
	Text text;

	const string ExercisingWithHeadphonesKey = "exercising_with_headphones_key";
	const string AllHeadphonesKey = "headphones_fence_key";
	const string AllLocationKey = "location_fence_key";
	const string AroundSunriseOrSunsetKey = "around_sunrise_or_sunset";
	const string WholeDayKey = "whole_day";
	const string NextHourKey = "next_hour";
	const string NextHourMonday = "next_hour_monday";

	const long HourInMillis = 60L * 60L * 1000L;
	const string AnyTimeIntervalKey = "any_time_interval";

	static long CurrentTimeMillis
	{
		get
		{
			var Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			var javaSpan = DateTime.UtcNow - Jan1970;
			return (long) javaSpan.TotalMilliseconds;
		}
	}

	#region fences_api

	[UsedImplicitly]
	public void OnQueryFences()
	{
		var allFences = FenceQueryRequest.All();
		var request = FenceQueryRequest.ForFences(AllHeadphonesKey, AllLocationKey);
		FenceClient.QueryFences(allFences, response => { }, LogFailure);
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
			.AddFence(ExercisingWithHeadphonesKey, exercisingWithHeadphonesFence)
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
		var aroundSunriseOrSunset = AroundSunriseOrSunsetFence();
		var wholeDay = WholeDay();
		var currentTimeMillis = CurrentTimeMillis;
		var nextHour = TimeFence.InInterval(currentTimeMillis, currentTimeMillis + HourInMillis);
		var nextHourMonday = TimeFence.InIntervalOfDay(TimeFence.DayOfWeek.Monday, 0L, 24L * HourInMillis);
		var anyTimeInterval = AnyTimeInterval();

		FenceClient.UpdateFences(new FenceUpdateRequest.Builder()
			.AddFence(AroundSunriseOrSunsetKey, aroundSunriseOrSunset)
			.AddFence(WholeDayKey, wholeDay)
			.AddFence(NextHourKey, nextHour)
			.AddFence(AnyTimeIntervalKey, anyTimeInterval)
			// throws FENCE_NOT_AVAILABLE for some reason
			//	.AddFence(NextHourMonday, nextHourMonday) 
			.Build(), OnUpdateFencesSuccess, OnUpdateFencesFailure);
	}

	static AwarenessFence AnyTimeInterval()
	{
		return AwarenessFence.Or(
			TimeFence.InTimeInterval(TimeFence.TimeInterval.Weekday),
			TimeFence.InTimeInterval(TimeFence.TimeInterval.Weekend),
			TimeFence.InTimeInterval(TimeFence.TimeInterval.Holiday),
			TimeFence.InTimeInterval(TimeFence.TimeInterval.Morning),
			TimeFence.InTimeInterval(TimeFence.TimeInterval.Afternoon),
			TimeFence.InTimeInterval(TimeFence.TimeInterval.Evening),
			TimeFence.InTimeInterval(TimeFence.TimeInterval.Night)
		);
	}

	static AwarenessFence WholeDay()
	{
		const long startTimeOFDayMillis = 0;
		const long endTimeOfDayMillis = 24L * HourInMillis;
		return TimeFence.InDailyInterval(startTimeOFDayMillis, endTimeOfDayMillis, "America/Denver");
	}

	static AwarenessFence AroundSunriseOrSunsetFence()
	{
		return AwarenessFence.Or(
			TimeFence.AroundTimeInstant(TimeFence.TimeInstant.Sunrise, -HourInMillis, HourInMillis),
			TimeFence.AroundTimeInstant(TimeFence.TimeInstant.Sunset, -HourInMillis, HourInMillis)
		);
	}

	[UsedImplicitly]
	public void OnRemoveAllFences()
	{
		FenceClient.UpdateFences(new FenceUpdateRequest.Builder()
			.RemoveFence(ExercisingWithHeadphonesKey)
			.RemoveFence(AllHeadphonesKey)
			.RemoveFence(AllLocationKey)
			.RemoveFence(AroundSunriseOrSunsetKey)
			.RemoveFence(WholeDayKey)
			.RemoveFence(NextHourKey)
			.RemoveFence(AnyTimeIntervalKey)
			.Build(), () => LogSuccess("Fences successfully removed"), OnUpdateFencesFailure);
	}

	void OnUpdateFencesFailure(string err)
	{
		LogFailure(err);
	}

	void OnUpdateFencesSuccess()
	{
		LogSuccess("Fences successfully updated");
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