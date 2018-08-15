using System;
using JetBrains.Annotations;

namespace DefaultNamespace
{
	[PublicAPI]
	public class FenceClient
	{
		public void updateFences(FenceUpdateRequest fenceUpdateRequest, Action onSuccess, Action<string> onFailure)
		{
			
		}
	}
}