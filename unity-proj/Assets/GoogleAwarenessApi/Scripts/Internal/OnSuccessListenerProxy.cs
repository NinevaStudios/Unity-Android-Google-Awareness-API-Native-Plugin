using System;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi.Internal
{
	public class OnSuccessListenerProxy : AndroidJavaProxy
	{
		readonly Action<HeadphoneState> _success;

		public OnSuccessListenerProxy(Action<HeadphoneState> success) : base("com.google.android.gms.tasks.OnSuccessListener")
		{
			_success = success;
		}

		[UsedImplicitly]
		void onSuccess(AndroidJavaObject result)
		{
			Debug.Log("onSuccess");
		}
	}
}