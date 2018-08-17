using System;
using DeadMosquito.GoogleMapsView.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi.Internal
{
	public class OnSuccessListenerProxy<T> : AndroidJavaProxy
	{
		readonly Action<T> _success;
		readonly Func<AndroidJavaObject, T> _converter;

		public OnSuccessListenerProxy([NotNull] Action<T> success, [NotNull] Func<AndroidJavaObject, T> converter)
			: base("com.google.android.gms.tasks.OnSuccessListener")
		{
			_success = success;
			_converter = converter;
		}

		[UsedImplicitly]
		void onSuccess(AndroidJavaObject result)
		{
			AwarenessSceneHelper.Queue(() => _success(_converter(result)));
		}
	}
}