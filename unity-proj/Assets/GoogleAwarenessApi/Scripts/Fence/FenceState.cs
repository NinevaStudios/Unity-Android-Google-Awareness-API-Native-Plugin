using System.Collections.Generic;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi.Internal;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	///     Represents the fence state
	/// </summary>
	[PublicAPI]
	public class FenceState
	{
		[PublicAPI]
		public enum State
		{
			/// <summary>
			///     Fence state is false.
			/// </summary>
			False = 1,

			/// <summary>
			///     Fence state is true.
			/// </summary>
			True = 2,

			/// <summary>
			///     Fence state is unknown, which can be due to no data received.
			/// </summary>
			Unknown = 0
		}

		/// <summary>
		///     Returns the fence key that identifies this fence in <see cref="AwarenessFence" />.
		/// </summary>
		public string FenceKey { get; private set; }

		/// <summary>
		///     Returns the last time the fence state was changed in milliseconds since epoch.
		/// </summary>
		public long LastFenceUpdateTimeMillis { get; private set; }

		/// <summary>
		///     Returns the current fence state.
		/// </summary>
		public State CurrentState { get; private set; }

		/// <summary>
		///     Returns the previous fence state.
		/// </summary>
		public State PreviousState { get; private set; }

		public static FenceState FromAJO(AndroidJavaObject ajo)
		{
			return new FenceState
			{
				FenceKey = ajo.CallStr("getFenceKey"),
				LastFenceUpdateTimeMillis = ajo.CallLong("getLastFenceUpdateTimeMillis"),
				CurrentState = (State) ajo.CallInt("getCurrentState"),
				PreviousState = (State) ajo.CallInt("getPreviousState")
			};
		}

		public override string ToString()
		{
			return string.Format("FenceKey: {0}, LastFenceUpdateTimeMillis: {1}, CurrentState: {2}, PreviousState: {3}", FenceKey, LastFenceUpdateTimeMillis, CurrentState, PreviousState);
		}

		public static FenceState FromJson(string fenceJson)
		{
			var json = Json.Deserialize(fenceJson) as Dictionary<string, object>;
			return new FenceState
			{
				FenceKey = json.GetStr("fenceKey"),
				LastFenceUpdateTimeMillis = json.GetLong("lastUpdateTime"),
				CurrentState = (State) json.GetInt("currentState"),
				PreviousState = (State) json.GetInt("previousState")
			};
		}
	}
}