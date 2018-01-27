using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour {

	public float timeBetweenSpawn;
	public GameObject puyoGameObject;
	public int blobBetween = 10;
	public float deltaReduceTime = 0.1f;
	public float MinGravityScale = 0.5f;
	public float MaxGravityScale = 1.5f;
	public float velocityMin = -10f;
	public float velocityMax = 10f;
	public int nbMinBySpawn = 1;
	public int nbMaxBySpawn = 3;
	public int torqueMin = -10;
	public int torqeMax = 10;
	
	private float timeLeftBeforeSpawn;

	private List<GameObject> puyoList;
	private List<GameObject> puyoOnGroundList;

	public bool isStart;

	private int xMin;
	private int xMax;
	private int nbPuyoSpawn;
	


	// Use this for initialization
	void Start () {
		puyoList = new List<GameObject> ();
		puyoOnGroundList = new List<GameObject>();

		xMin = (int) (transform.localPosition.x - transform.localScale.x / 2);
		xMax = (int) (transform.localPosition.x + transform.localScale.x / 2);
	}
	
	// Update is called once per frame
	void Update () {
		if (isStart)
		{
			timeLeftBeforeSpawn -= Time.deltaTime;

			if (timeLeftBeforeSpawn <= 0)
			{
				var nbSpawnRand = Random.Range(nbMinBySpawn, nbMaxBySpawn + 1);
				for (int i = 0; i < nbSpawnRand; i++)
				{
					spawnPuyo();	
				}
				if (nbPuyoSpawn >= blobBetween)
				{
					timeBetweenSpawn -= deltaReduceTime;
					if (timeBetweenSpawn < 0)
					{
						timeBetweenSpawn = 1;
					}
				}
				timeLeftBeforeSpawn = timeBetweenSpawn;
			}	
		}
	}

	private void spawnPuyo()
	{
		var velocityRand = Random.Range(velocityMin, velocityMax);
		var torqueRand = Random.Range(torqueMin, torqeMax);
		var xRand = Random.Range((float) xMin, (float) xMax);
		var newTransform = new Vector3(xRand, transform.position.y , transform.position.z);
		var puyo = Instantiate(puyoGameObject, newTransform, Quaternion.identity);

		var rigidbody2D = puyo.GetComponent<Rigidbody2D>();
		rigidbody2D.mass = Random.Range(MinGravityScale, MaxGravityScale);
		rigidbody2D.AddForce(new Vector2(velocityRand,0));
		rigidbody2D.AddTorque(torqueRand);
		puyoList.Add (puyo);
		nbPuyoSpawn++;
	}

	public List<GameObject> GetPuyoList()
	{
		return puyoList;
	}
	
	public List<GameObject> getOnGroundList()
	{
		foreach (var puyo in puyoList)
		{
			if (puyo.GetComponent<Puyo>().isOnGround)
				puyoOnGroundList.Add(puyo);
		}

		return puyoOnGroundList;
	}
	
}
