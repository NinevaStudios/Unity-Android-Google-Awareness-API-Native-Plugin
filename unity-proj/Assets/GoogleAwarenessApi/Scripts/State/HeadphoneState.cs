using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Headphone state.
	/// </summary>
	[PublicAPI]
	public enum HeadphoneState
	{
		/// <summary>
		/// Indicates that headphones are plugged into the physical headphone jack.
		/// </summary>
		PluggedIn = 1,

		/// <summary>
		/// Indicates that there are no headphones plugged into the physical headphone jack.
		/// </summary>
		Unplugged = 2
	}
}