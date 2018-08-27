using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Result for query fence states Api
	/// </summary>
	[PublicAPI]
	public class FenceQueryResponse
	{
		Dictionary<string, FenceState> _state = new Dictionary<string, FenceState>();

		Dictionary<string, FenceState> FenceStateDictionary
		{
			get { return _state; }
		}

		public static FenceQueryResponse FromAJO(AndroidJavaObject ajo)
		{
			var keysAjo = ajo.CallAJO("getFenceStateMap").CallAJO("getFenceKeys").FromJavaIterable<string>();
			keysAjo.ForEach(Debug.Log);
			return new FenceQueryResponse();
		}
	}
}