using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoOnGround : MonoBehaviour {

	private List<GameObject> puyoOnGroundList;
	
	// Use this for initialization
	void Start () {
		puyoOnGroundList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	List<GameObject> getOnGroundList(List<GameObject> puyoList)
	{
		foreach (var puyo in puyoList)
		{
			if (puyo.GetComponent<Puyo>().isOnGround)
				puyoOnGroundList.Add(puyo);
		}

		return puyoOnGroundList;
	}
}
