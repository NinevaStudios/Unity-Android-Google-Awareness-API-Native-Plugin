using System;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public class TimeFence
	{
		/// <summary>
		///     Represents day of week
		/// </summary>
		[PublicAPI]
		public enum DayOfWeek
		{
			/// <summary>
			///     Corresponds to Monday
			/// </summary>
			Monday = 2,

			/// <summary>
			///     Corresponds to Tuesday
			/// </summary>
			Tuesday = 3,

			/// <summary>
			///     Corresponds to Wednesday
			/// </summary>
			Wednesday = 4,

			/// <summary>
			///     Corresponds to Thursday
			/// </summary>
			Thursday = 5,

			/// <summary>
			///     Corresponds to Friday
			/// </summary>
			Friday = 6,

			/// <summary>
			///     Corresponds to Saturday
			/// </summary>
			Saturday = 7,

			/// <summary>
			///     Corresponds to Sunday
			/// </summary>
			Sunday = 1
		}

		/// <summary>
		///     Represents the time instant
		/// </summary>
		[PublicAPI]
		public enum TimeInstant
		{
			/// <summary>
			///     Denotes sunrise at the device location.
			/// </summary>
			Sunrise = 1,

			/// <summary>
			///     Denotes sunset at the device location.
			/// </summary>
			Sunset = 2
		}

		/// <summary>
		///     Represents the time interval
		/// </summary>
		[PublicAPI]
		public enum TimeInterval
		{
			/// <summary>
			///     Denotes a weekday for the device locale at the current time (internationalized properly).
			/// </summary>
			Weekday = 1,

			/// <summary>
			///     Denotes a weekend for the device locale at the current time (internationalized properly).
			/// </summary>
			Weekend = 2,

			/// <summary>
			///     Denotes a government-sanctioned holiday (implying that most schools and offices are closed) for the device locale at the current time (internationalized properly).
			/// </summary>
			Holiday = 3,

			/// <summary>
			///     Denotes the period of a day that is classified as morning (for example 8AM - 12 noon)
			/// </summary>
			Morning = 4,

			/// <summary>
			///     Denotes the period of a day that is classified as afternoon (for example 12 noon - 4PM)
			/// </summary>
			Afternoon = 5,

			/// <summary>
			///     Denotes the period of a day that is classified as evening (for example 4PM - 9PM)
			/// </summary>
			Evening = 6,

			/// <summary>
			///     Denotes the period of a day that is classified as night (for example 9PM - 8AM)
			/// </summary>
			Night = 7
		}

		const string TimeFenceClass = "com.google.android.gms.awareness.fence.TimeFence";
		const string TimeZoneClass = "java.util.TimeZone";

		/// <summary>
		///     This is a time fence that is in the <see cref="FenceState.State.True"/> state during a time interval defined with respect to <see cref="TimeInstant.Sunrise" /> or <see cref="TimeInstant.Sunset" /> instants.
		///     For example, if it is relative to <see cref="TimeInstant.Sunrise" />, and sunrise time is denoted by variable T, then this fence is in the <see cref="FenceState.State.True"/> state during the period [T + startOffsetMillis, T +
		///     stopOffsetMillis].
		/// </summary>
		/// <param name="timeInstant">is the desired semantic time label around which fence triggers are defined to happen.</param>
		/// <param name="startOffsetMillis">
		///     is the offset from the beginning of the semantic time period. It can be specified as a positive or negative offset value but should be between -24 to 24 hours
		///     inclusive (expressed in millis)
		/// </param>
		/// <param name="stopOffsetMillis">
		///     is the offset from the end of the semantic time period. It can be specified as a positive or negative offset value but should be between -24 to 24 hours inclusive
		///     (expressed in millis) constraint: <see cref="startOffsetMillis" /> < <see cref="stopOffsetMillis" />
		/// </param>
		/// <returns><see cref="AwarenessFence" /> that is <see cref="FenceState.State.True"/> when the current time falls within the interval specified based on the semantic time label and offsets.</returns>
		public static AwarenessFence AroundTimeInstant(TimeInstant timeInstant, long startOffsetMillis, long stopOffsetMillis)
		{
			return new AwarenessFence(TimeFenceClass.AJCCallStaticOnceAJO("aroundTimeInstant", (int) timeInstant, startOffsetMillis, stopOffsetMillis));
		}

		/// <summary>
		///     This fence is in the <see cref="FenceState.State.True"/> state during the interval specified by <see cref="startTimeOfDayMillis" /> and <see cref="stopTimeOfDayMillis" /> in the given <see cref="timeZone" />.
		/// </summary>
		/// <param name="startTimeOfDayMillis">Milliseconds since the start of the day. 12:00 am is 0L. The maximum value is the number of milliseconds in a day, namely 24L * 60L * 60L * 1000L.</param>
		/// <param name="stopTimeOfDayMillis">
		///     milliseconds since the start of the day. Same range as <see cref="startTimeOfDayMillis" />. This time must be greater than or equal to
		///     <see cref="startTimeOfDayMillis" />.
		/// </param>
		/// <param name="timeZone">
		///     The time zone to use. If set to null, current device time zone is used, and the fence re-adjusts to account for changes to the time zone (e.g. due to location change, or
		///     user-triggered change to Date & time settings).
		/// </param>
		/// <returns>
		///     <see cref="AwarenessFence" />
		/// </returns>
		public static AwarenessFence InDailyInterval(long startTimeOfDayMillis, long stopTimeOfDayMillis, string timeZone = null)
		{
			return new AwarenessFence(TimeFenceClass.AJCCallStaticOnceAJO("inDailyInterval", 
				ConvertTimeZone(timeZone), startTimeOfDayMillis, stopTimeOfDayMillis));
		}

		/// <summary>
		///     This fence is in the <see cref="FenceState.State.True"/> state when the current time is within the absolute times indicated by <see cref="startTimeMillis" /> and <see cref="stopTimeMillis" />.
		/// </summary>
		/// <param name="startTimeMillis">Milliseconds since epoch for the start of the interval. Must be greater than or equal to 0L.</param>
		/// <param name="stopTimeMillis">Milliseconds since epoch for the end of the interval. Must be greater than or equal to <see cref="startTimeMillis" />.</param>
		/// <returns>
		///     <see cref="AwarenessFence" />
		/// </returns>
		public static AwarenessFence InInterval(long startTimeMillis, long stopTimeMillis)
		{
			return new AwarenessFence(TimeFenceClass.AJCCallStaticOnceAJO("inInterval", startTimeMillis, stopTimeMillis));
		}

		/// <summary>
		///     This fence is in the <see cref="FenceState.State.True"/> state on <see cref="dayOfWeek" /> during the interval specified by <see cref="startTimeOfDayMillis" /> to <see cref="stopTimeOfDayMillis" /> in the given
		///     <see cref="timeZone" />.
		/// </summary>
		/// <param name="dayOfWeek">The day of the week.</param>
		/// <param name="startTimeOfDayMillis">Milliseconds since the start of the day. 12:00 am is 0L. The maximum value is the number of milliseconds in a day, namely 24L * 60L * 60L * 1000L.</param>
		/// <param name="stopTimeOfDayMillis">Milliseconds since the start of the day, This time must be greater than or equal to <see cref="startTimeOfDayMillis" />.</param>
		/// <param name="timeZone">
		///     The time zone to use. If set to null, current device time zone is used, and the fence re-adjusts to account for changes to the time zone (e.g. due to location change, or
		///     user-triggered change to Date & time settings).
		/// </param>
		/// <returns>
		///     <see cref="AwarenessFence" />
		/// </returns>
		public static AwarenessFence InIntervalOfDay(DayOfWeek dayOfWeek, long startTimeOfDayMillis, long stopTimeOfDayMillis, string timeZone = null)
		{
			return new AwarenessFence(TimeFenceClass.AJCCallStaticOnceAJO("inIntervalOfDay", (int) dayOfWeek, ConvertTimeZone(timeZone),
				startTimeOfDayMillis, startTimeOfDayMillis));
		}

		/// <summary>
		///     This fence is in the <see cref="FenceState.State.True"/> state if the day attributes for the present day/time is one of the attributes specified in the given dayAttributes
		/// </summary>
		/// <param name="timeInterval">is the desired attributes of the day for which this fence will trigger.</param>
		/// <returns>
		///     <see cref="AwarenessFence" /> that triggers when the day attributes of the present moment is the specified attribute.
		/// </returns>
		public static AwarenessFence InTimeInterval(TimeInterval timeInterval)
		{
			return new AwarenessFence(TimeFenceClass.AJCCallStaticOnceAJO("inTimeInterval", (int) timeInterval));
		}

		static AndroidJavaObject ConvertTimeZone(string timeZoneId)
		{
			if (timeZoneId == null)
			{
				return null;
			}

			return TimeZoneClass.AJCCallStaticOnceAJO("getTimeZone", timeZoneId);
		}
	}
}