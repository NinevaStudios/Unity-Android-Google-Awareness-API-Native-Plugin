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
		public static event Action<FenceState> OnFenceTriggered; 
		
		public const string AwarenessManagerClass = "com.ninevastudios.awareness.AwarenessManager";
		
		static AndroidJavaObject _client;

		/// <summary>
		/// Adds or removes a set of fences that are registered with the Awareness API.
		/// </summary>
		/// <param name="fenceUpdateRequest">A request indicating a batch of fences to add and/or remove.</param>
		/// <param name="onSuccess">Success callback.</param>
		/// <param name="onFailure">Failure callback.</param>
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

		/// <summary>
		/// Queries the state of a registered fence in the Awareness API.
		/// </summary>
		/// <param name="fenceQueryRequest">A request encapsulating the query criteria parameters.</param>
		/// <param name="onSuccess">Success callback.</param>
		/// <param name="onFailure">Failure callback.</param>
		public static void QueryFences(FenceQueryRequest fenceQueryRequest, Action<FenceQueryResponse> onSuccess, Action<string> onFailure)
		{
			if (CheckPreconditions())
			{
				return;
			}
			
			_client.CallAJO("queryFences", fenceQueryRequest.AJO)
				.CallAJO("addOnSuccessListener", new OnSuccessListenerProxy<FenceQueryResponse>(onSuccess, FenceQueryResponse.FromAJO))
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

			_client = AwarenessUtils.AwarenessClass.AJCCallStaticOnceAJO("getFenceClient", JniToolkitUtils.Activity);
			AwarenessManagerClass.AJCCallStaticOnce("register", JniToolkitUtils.Activity);
			AwarenessSceneHelper.Init();
		}

		public static void RaiseFenceEvent(FenceState fenceState)
		{
			if (OnFenceTriggered != null)
			{
				OnFenceTriggered(fenceState);
			}
		}
	}
}