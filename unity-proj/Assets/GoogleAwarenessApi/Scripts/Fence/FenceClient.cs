using System;
using JetBrains.Annotations;

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
		[PublicAPI]
		public class Builder
		{
			public Builder AddFence(string key, AwarenessFence fence)
			{
				// TODO 
				return this;
			}

			public Builder RemoveFence()
			{
				// TODO
				return this;
			}
			
			public FenceUpdateRequest Build()
			{
				return new FenceUpdateRequest();
			}
		}

		public static void UpdateFences(FenceUpdateRequest fenceUpdateRequest, Action onSuccess, Action<string> onFailure)
		{
		}
	}
}