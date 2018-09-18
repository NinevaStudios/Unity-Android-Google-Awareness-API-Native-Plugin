using System;
using System.Collections.Generic;
using DeadMosquito.GoogleMapsView.Internal;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi.Internal;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Class to help checking and requesting permissions
	/// </summary>
	[PublicAPI]
	public class PermissionHelper
	{
		const int PERMISSION_GRANTED = 0;
		const int PERMISSION_DENIED = -1;

		const string PermissionHelperActivityClass = "com.ninevastudios.awareness.PermissionHelperActivity";

		static Action<PermissionRequestResult> _callback;

		/// <summary>
		///     Permission status
		/// </summary>
		[PublicAPI]
		public enum PermissionStatus
		{
			/// <summary>
			///     The permission has been granted.
			/// </summary>
			[PublicAPI]
			Granted = PERMISSION_GRANTED,

			/// <summary>
			///     The permission has not been granted.
			/// </summary>
			[PublicAPI]
			Denied = PERMISSION_DENIED
		}

		/// <summary>
		///     Permission request result.
		/// </summary>
		[PublicAPI]
		public class PermissionRequestResult
		{
			/// <summary>
			///     Gets the requested permission.
			/// </summary>
			/// <value>The requested permission.</value>
			[PublicAPI]
			public string Permission { get; private set; }

			/// <summary>
			///     Gets the requested permission status.
			/// </summary>
			/// <value>The status of requested permssion.</value>
			[PublicAPI]
			public PermissionStatus Status { get; private set; }

			/// <summary>
			///     Gets whether you should show UI with rationale for requesting a permission.
			/// </summary>
			/// <value><c>true</c> if should show explanation why permission is needed; otherwise, <c>false</c>.</value>
			[PublicAPI]
			public bool ShouldShowRequestPermissionRationale { get; private set; }

			public static PermissionRequestResult FromJson(Dictionary<string, object> serialized)
			{
				var result = new PermissionRequestResult
				{
					Permission = serialized.GetStr("permission"),
					ShouldShowRequestPermissionRationale = (bool) serialized["shouldShowRequestPermissionRationale"],
					Status = (PermissionStatus) (int) (long) serialized["result"]
				};
				return result;
			}

			public override string ToString()
			{
				return String.Format(
					"[PermissionRequestResult: Permission={0}, Status={1}, ShouldShowRequestPermissionRationale={2}]",
					Permission, Status, ShouldShowRequestPermissionRationale);
			}
		}

		/// <summary>
		///     Checks if the ACCESS_FINE_LOCATION permission has been already granted by the user
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

		/// <summary>
		/// Request the ACCESS_FINE_LOCATION permission
		/// </summary>
		/// <param name="onRequestPermissionResult">Called when user chose whether to grant permission or not.</param>
		public static void RequestLocationPermission([NotNull] Action<PermissionRequestResult> onRequestPermissionResult)
		{
			if (onRequestPermissionResult == null)
			{
				throw new ArgumentNullException("onRequestPermissionResult");
			}

			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return;
			}

			_callback = onRequestPermissionResult;
			AwarenessSceneHelper.Init();
			PermissionHelperActivityClass.AJCCallStaticOnce("requestLocationPermission", JniToolkitUtils.Activity);
		}

		public static void TriggerCallback(string json)
		{
			if (_callback != null)
			{
				_callback(PermissionRequestResult.FromJson(Json.Deserialize(json) as Dictionary<string, object>));
			}
		}

		public static bool CheckLocationPermission()
		{
			if (!LocationPermissionGranted)
			{
				Debug.LogError("android.permission.ACCESS_FINE_LOCATION is not granted, use RequestLocationPermission to request it!");
				return true;
			}

			return false;
		}
	}
}