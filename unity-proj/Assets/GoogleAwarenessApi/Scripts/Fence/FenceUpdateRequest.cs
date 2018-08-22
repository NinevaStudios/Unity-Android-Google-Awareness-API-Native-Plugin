using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Request to add and/or remove multiple fences.
	/// </summary>
	[PublicAPI]
	public sealed class FenceUpdateRequest
	{
		public AndroidJavaObject AJO { get; private set; }

		public FenceUpdateRequest(AndroidJavaObject ajo)
		{
			AJO = ajo;
		}

		/// <summary>
		/// Builder for a <see cref="FenceUpdateRequest"/>.
		/// </summary>
		[PublicAPI]
		public class Builder
		{
			const string FenceUpdateRequestBuilderClass = "com.google.android.gms.awareness.fence.FenceUpdateRequest$Builder";
			readonly AndroidJavaObject _ajo;

			public Builder()
			{
				_ajo = new AndroidJavaObject(FenceUpdateRequestBuilderClass);
			}

			/// <summary>
			/// Adds a fence identified by the given key
			/// </summary>
			/// <param name="fenceKey"></param>
			/// <param name="fence">The fence that is to be registered.</param>
			/// <returns>This <see cref="FenceUpdateRequest.Builder"/> object.</returns>
			public Builder AddFence(string fenceKey, AwarenessFence fence)
			{
				var intent = FenceClient.AwarenessManagerClass.AJCCallStaticOnceAJO("getPendingIntent", JniToolkitUtils.Activity);
				_ajo.CallAJO("addFence", fenceKey, fence.AJO, intent);
				return this;
			}

			public Builder RemoveFence(string fenceKey)
			{
				_ajo.CallAJO("removeFence", fenceKey);
				return this;
			}

			public FenceUpdateRequest Build()
			{
				return new FenceUpdateRequest(_ajo.CallAJO("build"));
			}
		}
	}
}