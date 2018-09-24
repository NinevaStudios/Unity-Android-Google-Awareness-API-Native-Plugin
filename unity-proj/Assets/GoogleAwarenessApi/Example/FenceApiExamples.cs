using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;
using UnityEngine;
using UnityEngine.UI;

[SuppressMessage("ReSharper", "UnusedVariable")]
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

	const long HourInMillis = 60L * 60L * 1000L;
	const string AnyTimeIntervalKey = "any_time_interval";
	const string AllBeaconFence = "all_beacon_fence";

	static long CurrentTimeMillis
	{
		get
		{
			var Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			var javaSpan = DateTime.UtcNow - Jan1970;
			return (long) javaSpan.TotalMilliseconds;
		}
	}

	void Start()
	{
		FenceClient.OnFenceTriggered += LogSuccess;
	}

	#region fences_api

	/// <summary>
	/// Query for which fences are enabled
	/// </summary>
	[UsedImplicitly]
	public void OnQueryFences()
	{
#pragma warning disable 0219
		var request = FenceQueryRequest.ForFences(AllHeadphonesKey, AllLocationKey);
#pragma warning restore 0219

		FenceClient.QueryFences(FenceQueryRequest.All(), response =>
		{
			// This callback will be executed with all fences that are currently active
			var sb = new StringBuilder();
			sb.Append("Active fences: ");
			foreach (var fenceState in response.FenceStateDictionary)
			{
				sb.AppendFormat("{0} : {1}\n", fenceState.Key, fenceState.Value);
			}

			LogSuccess(sb);
		}, LogFailure);
	}

	[UsedImplicitly]
	public void OnRegisterFences()
	{
		var currentTimeMillis = CurrentTimeMillis;

		FenceClient.UpdateFences(new FenceUpdateRequest.Builder()
			.AddFence(ExercisingWithHeadphonesKey, CreateExercisingWithHeadphonesFence())
			.AddFence(AllHeadphonesKey, CreateHeadphonesFence())
			.AddFence(AllLocationKey, CreateLocationFence())
			.AddFence(AllBeaconFence, CreateBeaconFence())
			.AddFence(AroundSunriseOrSunsetKey, CreateSunriseOrSunsetFence())
			.AddFence(AnyTimeIntervalKey, CreateAnyTimeIntervalFence())
			.AddFence(WholeDayKey, CreateWholeDayFence())
			.AddFence(NextHourKey, TimeFence.InInterval(currentTimeMillis, currentTimeMillis + HourInMillis))
			// throws FENCE_NOT_AVAILABLE for some reason
//			.AddFence("next_hour_monday, TimeFence.InIntervalOfDay(TimeFence.DayOfWeek.Monday, 0L, 24L * HourInMillis))
			.Build(), () => { LogSuccess("Fences successfully updated"); }, LogFailure);
	}

	static AwarenessFence CreateExercisingWithHeadphonesFence()
	{
		// DetectedActivityFence will fire when it detects the user performing the specified
		// activity.  In this case it's walking.
		var walkingFence = DetectedActivityFence.During(DetectedActivityFence.ActivityType.Walking);

		// There are lots of cases where it's handy for the device to know if headphones have been
		// plugged in or unplugged.  For instance, if a music app detected your headphones fell out
		// when you were in a library, it'd be pretty considerate of the app to pause itself before
		// the user got in trouble.
		var headphoneFence = HeadphoneFence.During(HeadphoneState.PluggedIn);

#pragma warning disable 0219
		// Combines multiple fences into a compound fence.  While the first two fences trigger
		// individually, this fence will only trigger its callback when all of its member fences
		// hit a true state.
		var notWalkingWithHeadphones = AwarenessFence.And(AwarenessFence.Not(walkingFence), headphoneFence);
#pragma warning restore 0219

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

		return exercisingWithHeadphonesFence;
	}

	static AwarenessFence CreateHeadphonesFence()
	{
		// Will trigger when headphones are connected or disconnected
		return AwarenessFence.Or(HeadphoneFence.During(HeadphoneState.PluggedIn), HeadphoneFence.PluggingIn(), HeadphoneFence.Unplugging());
	}

	static AwarenessFence CreateLocationFence()
	{
		return AwarenessFence.Or(
			LocationFence.Entering(0, 0, 1000),
			LocationFence.Exiting(0, 0, 1000),
			LocationFence.In(0, 0, 1000, 100)
		);
	}

	static AwarenessFence CreateBeaconFence()
	{
		var beaconTypes = new List<BeaconState.TypeFilter> {BeaconState.TypeFilter.With("awareness-api-1534415879510", "string")};
		var found = BeaconFence.Found(beaconTypes);
		var near = BeaconFence.Near(beaconTypes);
		var lost = BeaconFence.Lost(beaconTypes);
		return AwarenessFence.Or(found, near, lost);
	}

	static AwarenessFence CreateAnyTimeIntervalFence()
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

	static AwarenessFence CreateWholeDayFence()
	{
		const long startTimeOFDayMillis = 0;
		const long endTimeOfDayMillis = 24L * HourInMillis;
		return TimeFence.InDailyInterval(startTimeOFDayMillis, endTimeOfDayMillis, "America/Denver");
	}

	static AwarenessFence CreateSunriseOrSunsetFence()
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
			.Build(), () => LogSuccess("Fences successfully removed"), LogFailure);
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