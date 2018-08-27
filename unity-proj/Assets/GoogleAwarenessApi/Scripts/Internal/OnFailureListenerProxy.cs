using System;
using DeadMosquito.GoogleMapsView.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi.Internal
{
	public class OnFailureListenerProxy : AndroidJavaProxy
	{
		readonly Action<string> _failure;

		public OnFailureListenerProxy(Action<string> failure) : base("com.google.android.gms.tasks.OnFailureListener")
		{
			_failure = failure;
		}

		[UsedImplicitly]
		void onFailure(AndroidJavaObject exception)
		{
			AwarenessSceneHelper.Queue(() => _failure(exception.JavaToString()));
		}
	}
}