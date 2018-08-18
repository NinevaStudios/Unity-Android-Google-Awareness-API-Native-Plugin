using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;
using UnityEngine;

public class AwarenessExamples : MonoBehaviour {

	#region snaphsot_API
	
	[UsedImplicitly]
	public void OnGetDetectedActivity()
	{
		SnapshotClient.GetDetectedActivity(activity => Debug.Log(activity), Debug.LogError);
	}

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
	
	[UsedImplicitly]
	public void OnGetNearbyPlaces()
	{
		SnapshotClient.GetPlaces(places => places.ForEach(Debug.Log), Debug.LogError);
	}
	
	[UsedImplicitly]
	public void OnGetBeaconState()
	{
		SnapshotClient.GetBeaconState(beaconState => Debug.Log(beaconState), Debug.LogError);
	}

	#endregion
}
