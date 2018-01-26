using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour {

	public float timeBetweenSpawn;
	public GameObject puyoGameObject;
	
	private float timeLeftBeforeSpawn;

	List<GameObject> blobList;

	// Use this for initialization
	void Start () {
		blobList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeLeftBeforeSpawn -= Time.deltaTime;
		if (timeLeftBeforeSpawn <= 0)
		{
			var blob = Instantiate(puyoGameObject, transform);
			blobList.Add (blob);
			timeLeftBeforeSpawn = timeBetweenSpawn;
		}
	}
	
	
}
