using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Semantic time intervals for the to the current time and location.
	/// </summary>
	[PublicAPI]
	public enum TimeInterval
	{
		/// <summary>
		/// Denotes the period of a day that is classified as morning (for example 8AM - 12 noon)
		/// </summary>
		Morning = 4,

		/// <summary>
		/// Denotes the period of a day that is classified as afternoon (for example 12 noon - 4PM)
		/// </summary>
		Afternoon = 5,

		/// <summary>
		/// Denotes the period of a day that is classified as evening (for example 4PM - 9PM)
		/// </summary>
		Evening = 6,

		/// <summary>
		/// Denotes the period of a day that is classified as night (for example 9PM - 8AM)
		/// </summary>
		Night = 7,

		/// <summary>
		/// Denotes a weekday for the device locale at the current time (internationalized properly). As an example, in the U.S., weekdays are understood to be Monday, Tuesday, Wednesday, Thursday, Friday.
		/// </summary>
		Weekday = 1,

		/// <summary>
		/// Denotes a weekend for the device locale at the current time (internationalized properly). As an example, in the U.S., weekdays are understood to be Saturday and Sunday.
		/// </summary>
		Weekend = 2,

		/// <summary>
		/// Denotes a government-sanctioned holiday (implying that most schools and offices are closed) for the device locale at the current time (internationalized properly). As an example, in the U.S., some examples of holidays are Thanksgiving day, Christmas day, Independence day.
		/// </summary>
		Holiday = 3
	}
}