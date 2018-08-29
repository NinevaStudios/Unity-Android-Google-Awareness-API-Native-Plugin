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

		public Dictionary<string, FenceState> FenceStateDictionary
		{
			get { return _state; }
		}

		public static FenceQueryResponse FromAJO(AndroidJavaObject ajo)
		{
			var fenceQueryResponse = new FenceQueryResponse();
			var stateMapAjo = ajo.CallAJO("getFenceStateMap");
			var keys = stateMapAjo.CallAJO("getFenceKeys").FromJavaIterable<string>();
			keys.ForEach(key => fenceQueryResponse.FenceStateDictionary[key] = FenceState.FromAJO(stateMapAjo.CallAJO("getFenceState", key)));
			return fenceQueryResponse;
		}
	}
}