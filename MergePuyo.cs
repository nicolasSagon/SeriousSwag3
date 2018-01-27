using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

public class MergePuyo : MonoBehaviour
{
	public GameObject mergedPuyo;

	public GameObject input;
	// Use this for initialization
	void Start () {
/* TEST CODE		
		mergedPuyo = Instantiate(input);
		
		GameObject puyo1 = Instantiate(input);
		GameObject puyo2 = Instantiate(input);
		
		puyo1.GetComponent<Puyo>().colorValue = Color.blue;
		puyo2.GetComponent<Puyo>().colorValue = Color.yellow;
		puyo1.transform.localScale=Vector3.one;
		puyo2.transform.localScale=Vector3.one;
		
		List<GameObject> puyoList = new List<GameObject> ();
		
		puyoList.Add(puyo1);
		puyoList.Add(puyo2);

		mergedPuyo = mergePuyo(puyoList);
		//destroyPuyoOnGroundList();

		Debug.Log(mergedPuyo.ToString());
 */
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	GameObject mergePuyo(List<GameObject> puyoOnGroundList)
	{
		mergedPuyo.transform.localScale = mergeSize(puyoOnGroundList);
		mergedPuyo.GetComponent<Puyo>().colorValue = mergeColor(puyoOnGroundList);

		return mergedPuyo;
	}

	Color mergeColor(List<GameObject> puyoOnGroundList)
	{
		Color mergedPuyoColor = new Color();
		foreach (var puyo in puyoOnGroundList)
		{
			mergedPuyoColor += puyo.GetComponent<Puyo>().colorValue;
		}

		mergedPuyoColor /= puyoOnGroundList.Count;
		
		return mergedPuyoColor;
	}
	
	Vector3 mergeSize(List<GameObject> puyoOnGroundList)
	{
		Vector3 mergedPuyoSize = new Vector3();
		foreach (var puyo in puyoOnGroundList)
		{
			mergedPuyoSize += puyo.transform.localScale;
		}
		
		return mergedPuyoSize;
	}
}
