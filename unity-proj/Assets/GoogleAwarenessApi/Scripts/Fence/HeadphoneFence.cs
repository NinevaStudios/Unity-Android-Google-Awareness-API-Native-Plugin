using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public class HeadphoneFence
	{
		const string HeadphoneFenceClass = "com.google.android.gms.awareness.fence.HeadphoneFence";

		public static AwarenessFence During(HeadphoneState headphoneState)
		{
			return new AwarenessFence(HeadphoneFenceClass.AJCCallStaticOnceAJO("during", (int) headphoneState));
		}
		
		public static AwarenessFence PluggingIn()
		{
			return new AwarenessFence(HeadphoneFenceClass.AJCCallStaticOnceAJO("pluggingIn"));
		}
		
		public static AwarenessFence Unplugging()
		{
			return new AwarenessFence(HeadphoneFenceClass.AJCCallStaticOnceAJO("unplugging"));
		}
	}
}