using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour {

	public float timeLeftBeforeSpawn;
	public float timeBetweenSpawn;

	List<GameObject> blobList;

	// Use this for initialization
	void Start () {
		blobList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {

		timeLeftBeforeSpawn -= Time.deltaTime;
		if (timeLeftBeforeSpawn <= 0) {
			GameObject blob = new GameObject ();
			blobList.Add (blob);
			timeLeftBeforeSpawn = timeBetweenSpawn;
		}
		//Debug.Log(blobList.Count.ToString());
	}
}
