using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoalPuyo : MonoBehaviour {

	public GameObject goalPuyo;
	public int minSize = 4;
	public int maxSize = 16;
	
	public GameObject input;
	public Color color;
	
	// Use this for initialization
	void Start () {
		// TEST CODE
		 
		goalPuyo = Instantiate(input);
		generateGoalPuyo();

		Debug.Log("color = " + color);
		Debug.Log("size = " + goalPuyo.transform.localScale);
		
	}
	
	// Update is called once per frame
	void Update () {
		goalPuyo.GetComponent<Puyo>().ChangeColor(color);
	}

	void generateGoalPuyo()
	{
		goalPuyo.transform.localScale = generateGoalPuyoSize();
		color = generateGoalPuyoColor();
	}
	
	Color generateGoalPuyoColor()
	{
		Color goalPuyoColor = new Color();
		
		goalPuyoColor = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
		goalPuyo.GetComponent<Renderer>().material.color = goalPuyoColor;
		
		return goalPuyoColor;
	}
	
	Vector3 generateGoalPuyoSize()
	{
		Vector3 goalPuyoSize = new Vector3();
		int goalSize = Random.Range(minSize, maxSize + 1);
		//double goalVolume = Math.Pow(goalSize, (double) 1 / 3);
		double goalVolume = Math.Sqrt(goalSize);
		goalPuyoSize.Set((float) goalVolume, (float) goalVolume, (float) goalVolume);
		
		return goalPuyoSize;
	}
}
