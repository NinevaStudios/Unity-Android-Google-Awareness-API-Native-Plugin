using System.Collections.Generic;
using System.Linq;
using GoogleAwarenessApi.Scripts.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Result of an activity recognition.
	///
	/// It contains a list of activities that a user may have been doing at a particular time. The activities are sorted by the most probable activity first. A confidence is associated with each activity which indicates how likely that activity is.
	/// </summary>
	[PublicAPI]
	public sealed class ActivityRecognitionResult
	{
		public ActivityRecognitionResult(long timestamp, long elapsedRealtimeMillis, List<DetectedActivity> probableActivities)
		{
			Timestamp = timestamp;
			ElapsedRealtimeMillis = elapsedRealtimeMillis;
			ProbableActivities = probableActivities;
		}

		/// <summary>
		/// Returns the UTC time of this detection, in milliseconds since January 1, 1970.
		/// </summary>
		public long Timestamp { get; private set; }

		/// <summary>
		/// Returns the elapsed real time of this detection in milliseconds since boot, including time spent in sleep as obtained by SystemClock.elapsedRealtime().
		/// </summary>
		public long ElapsedRealtimeMillis { get; private set; }

		/// <summary>
		/// Returns the most probable activity of the user.
		/// </summary>
		public DetectedActivity MostProbableActivity
		{
			get { return ProbableActivities.OrderByDescending(x => x.Confidence).FirstOrDefault(); }
		}

		/// <summary>
		/// Returns the list of activities that were detected with the confidence value associated with each activity.
		/// </summary>
		public List<DetectedActivity> ProbableActivities { get; private set; }

		/// <summary>
		/// Returns the confidence of the given activity type.
		/// </summary>
		/// <param name="activityActivityType">Activity type</param>
		/// <returns>Returns the confidence of the given activity type.</returns>
		public int GetActivityConfidence(DetectedActivity.ActivityType activityActivityType)
		{
			return ProbableActivities.Where(x => x.ActivityActivityType == activityActivityType).Select(x => x.Confidence).FirstOrDefault();
		}

		public static ActivityRecognitionResult FromAJO(AndroidJavaObject ajo)
		{
			var probableActivities = ajo.CallAJO("getProbableActivities").FromJavaList(
				x => new DetectedActivity(x.CallInt("getConfidence"), (DetectedActivity.ActivityType) x.CallInt("getType")));
			return new ActivityRecognitionResult(ajo.CallLong("getTime"), ajo.CallLong("getElapsedRealtimeMillis"), probableActivities);
		}

		public override string ToString()
		{
			return string.Format("Timestamp: {0}, ElapsedRealtimeMillis: {1}, MostProbableActivity: {2}, ProbableActivities: {3}",
				Timestamp, ElapsedRealtimeMillis, MostProbableActivity, ProbableActivities.CommaJoin());
		}
	}
}