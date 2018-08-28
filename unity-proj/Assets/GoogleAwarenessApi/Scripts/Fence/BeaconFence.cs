using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Use this class to create beacon fences.
	///
	/// The fences in this class support detecting nearby beacons that are associated with attachments, which are a triple of namespace, type, and content.
	///
	/// Note: Values that indicate a changing state are momentarily TRUE for about 5 seconds, then automatically revert to FALSE.
	/// </summary>
	[PublicAPI]
	public class BeaconFence
	{
		
	}
}