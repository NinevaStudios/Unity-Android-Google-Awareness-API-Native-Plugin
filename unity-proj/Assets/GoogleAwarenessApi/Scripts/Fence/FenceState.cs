using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Represents the fence state
	/// </summary>
	[PublicAPI]
	public enum FenceState
	{
		/// <summary>
		/// Fence state is false.
		/// </summary>
		False = 1,

		/// <summary>
		/// Fence state is true.
		/// </summary>
		True = 2,

		/// <summary>
		/// Fence state is unknown, which can be due to no data received.
		/// </summary>
		Unknown = 3
	}
}