using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
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
			return base.ToString();
		}
	}
}