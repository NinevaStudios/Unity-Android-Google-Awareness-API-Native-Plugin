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
		Dictionary<string, FenceState> FenceStateDictionary
		{
			get { return null; }
		}

		public static FenceQueryResponse FromAJO(AndroidJavaObject ajo)
		{
			throw new System.NotImplementedException();
		}
	}
}