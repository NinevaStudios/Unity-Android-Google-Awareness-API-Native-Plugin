using GoogleAwarenessApi.Scripts.Internal;
using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Represents time intervals of the day
	/// </summary>
	[PublicAPI]
	public class TimeIntervals
	{
		public TimeInterval[] CurrentTimeIntervals { get; private set; }

		public TimeIntervals(TimeInterval[] intervals)
		{
			CurrentTimeIntervals = intervals;
		}

		public override string ToString()
		{
			return CurrentTimeIntervals.CommaJoin();
		}
	}
}