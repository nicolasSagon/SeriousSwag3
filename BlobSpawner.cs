using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour {

	public float timeLeftBeforeSpawn;
	public float timeBetweenSpawn;

	private List<GameObject> puyoList;

	// Use this for initialization
	void Start () {
		puyoList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {

		timeLeftBeforeSpawn -= Time.deltaTime;
		if (timeLeftBeforeSpawn <= 0) {
			GameObject puyo = new GameObject ();
			puyoList.Add (puyo);
			timeLeftBeforeSpawn = timeBetweenSpawn;
		}
	}
}
