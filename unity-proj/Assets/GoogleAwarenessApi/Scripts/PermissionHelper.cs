using DeadMosquito.GoogleMapsView.Internal;
using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public class PermissionHelper
	{
		const string PermissionHelperActivityClass = "com.ninevastudios.awareness.PermissionHelperActivity";
		
		/// <summary>
		/// Checks if the ACCESS_FINE_LOCATION permission has been already granted by the user
		/// </summary>
		public static bool LocationPermissionGranted
		{
			get
			{
				if (JniToolkitUtils.IsNotAndroidRuntime)
				{
					return true;
				}
				
				return PermissionHelperActivityClass.AJCCallStaticOnce<bool>("isLocationPermissionGranted", JniToolkitUtils.Activity);
			}
		}
		
		public static void RequestLocationPermission()
		{
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return;
			}
			
			AwarenessSceneHelper.Init();
			PermissionHelperActivityClass.AJCCallStaticOnce("requestLocationPermission", JniToolkitUtils.Activity);
		}
	}
}