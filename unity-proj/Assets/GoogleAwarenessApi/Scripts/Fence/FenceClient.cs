using System;
using DeadMosquito.GoogleMapsView.Internal;
using GoogleAwarenessApi.Scripts.Internal;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi.Internal;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Main class to interact with fence API
	///
	/// See https://developers.google.com/android/reference/com/google/android/gms/awareness/FenceClient
	/// </summary>
	[PublicAPI]
	public class FenceClient
	{
		static AndroidJavaObject _client;

		public static void UpdateFences(FenceUpdateRequest fenceUpdateRequest, Action onSuccess, Action<string> onFailure)
		{
			if (CheckPreconditions())
			{
				return;
			}

			Action<AndroidJavaObject> wrapper = _ => onSuccess();
			_client.CallAJO("updateFences", fenceUpdateRequest.AJO)
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy<AndroidJavaObject>(wrapper, ajo => ajo))
				.CallAJO("addOnFailureListener", new OnFailureListenerProxy(onFailure));
		}
		
		static bool CheckPreconditions()
		{
			if (JniToolkitUtils.IsNotAndroidRuntime)
			{
				return true;
			}

			CreateClientLazy();
			return false;
		}

		static void CreateClientLazy()
		{
			if (_client != null)
			{
				return;
			}

			_client = AwarenessUtils.AwarenessClass.AJCCallStaticOnceAJO("getSnapshotClient", JniToolkitUtils.Activity);
			AwarenessSceneHelper.Init();
		}
	}
}