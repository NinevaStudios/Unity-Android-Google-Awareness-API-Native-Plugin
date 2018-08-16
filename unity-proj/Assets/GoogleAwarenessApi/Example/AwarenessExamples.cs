using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;
using UnityEngine;

public class AwarenessExamples : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[UsedImplicitly]
	public void OnGetHeadphonesState()
	{
		SnapshotClient.GetHeadphoneState(state => Debug.Log(state), Debug.LogError);
	}
}
