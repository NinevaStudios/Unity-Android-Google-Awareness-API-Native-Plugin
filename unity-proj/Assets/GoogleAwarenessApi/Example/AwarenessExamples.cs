using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;
using UnityEngine;

public class AwarenessExamples : MonoBehaviour {

	#region snaphsot_API

	[UsedImplicitly]
	public void OnGetHeadphonesState()
	{
		SnapshotClient.GetHeadphoneState(state => Debug.Log(state), Debug.LogError);
	}
	
	[UsedImplicitly]
	public void OnGetWeather()
	{
		SnapshotClient.GetWeather(Debug.Log, Debug.LogError);
	}
	
	[UsedImplicitly]
	public void OnGetLocation()
	{
		SnapshotClient.GetLocation(weather => Debug.Log(weather), Debug.LogError);
	}
	
	[UsedImplicitly]
	public void OnGetTimeIntervals()
	{
		SnapshotClient.GetTimeIntervals(intervals => Debug.Log(intervals), Debug.LogError);
	}

	#endregion
}
