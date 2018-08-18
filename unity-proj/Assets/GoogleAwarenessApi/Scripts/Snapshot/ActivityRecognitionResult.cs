using System.Collections.Generic;
using System.Linq;
using GoogleAwarenessApi.Scripts.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public sealed class ActivityRecognitionResult
	{
		public long Timestamp { get; private set; }
		public long ElapsedRealtimeMillis { get; private set; }

		public DetectedActivity MostProbableActivity
		{
			get { return ProbableActivities.OrderByDescending(x => x.Confidence).FirstOrDefault(); }
		}

		public List<DetectedActivity> ProbableActivities { get; private set; }

		public ActivityRecognitionResult(long timestamp, long elapsedRealtimeMillis, List<DetectedActivity> probableActivities)
		{
			Timestamp = timestamp;
			ElapsedRealtimeMillis = elapsedRealtimeMillis;
			ProbableActivities = probableActivities;
		}

		public static ActivityRecognitionResult FromAJO(AndroidJavaObject ajo)
		{
			var probableActivities = ajo.CallAJO("getProbableActivities").FromJavaList(
				x => new DetectedActivity(x.CallInt("getConfidence"), (DetectedActivity.Type) x.CallInt("getType")));
			return new ActivityRecognitionResult(ajo.CallLong("getTime"), ajo.CallLong("getElapsedRealtimeMillis"), probableActivities);
		}

		public int GetActivityConfidence(DetectedActivity.Type activityType)
		{
			return ProbableActivities.Where(x => x.ActivityType == activityType).Select(x => x.Confidence).FirstOrDefault();
		}

		public override string ToString()
		{
			return string.Format("Timestamp: {0}, ElapsedRealtimeMillis: {1}, MostProbableActivity: {2}, ProbableActivities: {3}",
				Timestamp, ElapsedRealtimeMillis, MostProbableActivity, ProbableActivities.CommaJoin());
		}
	}
}