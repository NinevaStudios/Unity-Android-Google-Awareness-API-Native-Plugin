using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Use this class to create headphone state fences.
	/// </summary>
	[PublicAPI]
	public class HeadphoneFence
	{
		const string HeadphoneFenceClass = "com.google.android.gms.awareness.fence.HeadphoneFence";

		/// <summary>
		/// This fence is in the <see cref="FenceState.State.True"/> state when the headphones are in the specified state.
		/// </summary>
		/// <param name="headphoneState">Headphone state</param>
		/// <returns></returns>
		public static AwarenessFence During(HeadphoneState headphoneState)
		{
			return new AwarenessFence(HeadphoneFenceClass.AJCCallStaticOnceAJO("during", (int) headphoneState));
		}
		
		/// <summary>
		/// This fence is momentarily (about 5 seconds) in the <see cref="FenceState.State.True"/> state when headphones are plugged in to the device.
		/// </summary>
		/// <returns><see cref="AwarenessFence"/></returns>
		public static AwarenessFence PluggingIn()
		{
			return new AwarenessFence(HeadphoneFenceClass.AJCCallStaticOnceAJO("pluggingIn"));
		}
		
		/// <summary>
		/// This fence is momentarily (about 5 seconds) in the <see cref="FenceState.State.True"/> state when headphones are unplugged from the device.
		/// </summary>
		/// <returns><see cref="AwarenessFence"/></returns>
		public static AwarenessFence Unplugging()
		{
			return new AwarenessFence(HeadphoneFenceClass.AJCCallStaticOnceAJO("unplugging"));
		}
	}
}