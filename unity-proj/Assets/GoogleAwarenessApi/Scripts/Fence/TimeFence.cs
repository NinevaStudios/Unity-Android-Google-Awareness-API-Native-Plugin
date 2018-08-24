using System;
using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public class TimeFence
	{
		const string TimeFenceClass = "com.google.android.gms.awareness.fence.TimeFence";
		const string TimeZoneClass = "java.util.TimeZone";

		/// <summary>
		/// This is a time fence that is in the TRUE state during a time interval defined with respect to <see cref="TimeInstant.Sunrise"/> or <see cref="TimeInstant.Sunset"/> instants.
		///
		/// For example, if it is relative to <see cref="TimeInstant.Sunrise"/>, and sunrise time is denoted by variable T, then this fence is in the TRUE state during the period [T + startOffsetMillis, T + stopOffsetMillis].
		/// </summary>
		/// <param name="timeInstant">is the desired semantic time label around which fence triggers are defined to happen.</param>
		/// <param name="startOffsetMillis">is the offset from the beginning of the semantic time period. It can be specified as a positive or negative offset value but should be between -24 to 24 hours inclusive (expressed in millis)</param>
		/// <param name="stopOffsetMillis">is the offset from the end of the semantic time period. It can be specified as a positive or negative offset value but should be between -24 to 24 hours inclusive (expressed in millis) constraint: startOffsetMillis < stopOffsetMillis</param>
		/// <returns><see cref="AwarenessFence"/> that is TRUE when the current time falls within the interval specified based on the semantic time label and offsets.</returns>
		public static AwarenessFence AroundTimeInstant(TimeInstant timeInstant, long startOffsetMillis, long stopOffsetMillis)
		{
			return new AwarenessFence(TimeFenceClass.AJCCallStaticOnceAJO("aroundTimeInstant", (int) timeInstant, startOffsetMillis, stopOffsetMillis));
		}

		/// <summary>
		/// This fence is in the TRUE state during the interval specified by <see cref="startTimeOfDayMillis"/> and <see cref="stopTimeOfDayMillis"/> in the given <see cref="timeZone"/>.
		/// </summary>
		/// <param name="timeZone">	The time zone to use. If set to null, current device time zone is used, and the fence re-adjusts to account for changes to the time zone (e.g. due to location change, or user-triggered change to Date & time settings).</param>
		/// <param name="startTimeOfDayMillis">Milliseconds since the start of the day. 12:00 am is 0L. The maximum value is the number of milliseconds in a day, namely 24L * 60L * 60L * 1000L.</param>
		/// <param name="stopTimeOfDayMillis">milliseconds since the start of the day. Same range as <see cref="startTimeOfDayMillis"/>. This time must be greater than or equal to <see cref="startTimeOfDayMillis"/>.</param>
		/// <returns></returns>
		public static AwarenessFence InDailyInterval(TimeZone timeZone, long startTimeOfDayMillis, long stopTimeOfDayMillis)
		{
			// TODO
			// https://stackoverflow.com/questions/35794249/how-to-get-time-zone-as-a-string-in-c-sharp
			return new AwarenessFence(null);
		}

		public static AwarenessFence InIntervalOfDay()
		{
			// TODO
			return new AwarenessFence(null);
		}

		/// <summary>
		/// Represents the time instant
		/// </summary>
		[PublicAPI]
		public enum TimeInstant
		{
			/// <summary>
			/// Denotes sunrise at the device location.
			/// </summary>
			Sunrise = 1,
			
			/// <summary>
			/// Denotes sunset at the device location.
			/// </summary>
			Sunset = 2
		}

		/// <summary>
		/// Represents day of week
		/// </summary>
		[PublicAPI]
		public enum DayOfWeek
		{
			/// <summary>
			/// Corresponds to Monday
			/// </summary>
			Monday = 2,

			/// <summary>
			/// 	Corresponds to Tuesday
			/// </summary>
			Tuesday = 3,

			/// <summary>
			/// Corresponds to Wednesday
			/// </summary>
			Wednesday = 4,

			/// <summary>
			/// Corresponds to Thursday
			/// </summary>
			Thursday = 5,

			/// <summary>
			/// Corresponds to Friday
			/// </summary>
			Friday = 6,

			/// <summary>
			/// Corresponds to Saturday
			/// </summary>
			Saturday = 7,

			/// <summary>
			/// Corresponds to Sunday
			/// </summary>
			Sunday = 1
		}
	}
}