using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public class TimeFence
	{
		public static AwarenessFence InIntervalOfDay()
		{
			// TODO
			return new AwarenessFence(null);
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