using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour {

	public float timeBetweenSpawn;
	public GameObject puyoGameObject;
	
	private float timeLeftBeforeSpawn;

	private List<GameObject> puyoList;

	public bool isStart;

	// Use this for initialization
	void Start () {
		puyoList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isStart)
		{
			timeLeftBeforeSpawn -= Time.deltaTime;

			if (timeLeftBeforeSpawn <= 0) {
				var puyo = Instantiate(puyoGameObject, transform);
				puyoList.Add (puyo);
				timeLeftBeforeSpawn = timeBetweenSpawn;
			}	
		}
	}

	public List<GameObject> GetPuyoList()
	{
		return puyoList;
	}
	
	
}
