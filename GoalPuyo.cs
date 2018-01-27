using System;
using System.Collections;
using System.Collections.Generic;
using SeriousSwag3.Utils;
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
		goalPuyo.GetComponent<Puyo>().setColor(color);
	}

	void generateGoalPuyo()
	{
		int numberOfPuyoNeeded = Random.Range(minSize, maxSize + 1);
		goalPuyo.transform.localScale = generateGoalPuyoSize(numberOfPuyoNeeded);
		color = generateGoalPuyoColor(numberOfPuyoNeeded);
	}
	
	Color generateGoalPuyoColor(int numberOfPuyoNeeded)
	{
		/*Color goalPuyoColor = new Color();
		
		goalPuyoColor = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
		goalPuyo.GetComponent<Renderer>().material.color = goalPuyoColor;
		
		return goalPuyoColor;*/
		Color goalPuyoColor = new Color();

		goalPuyoColor = ColorGenerator.GetColorFromHex(ColorGenerator.GetRandomColor());

		for (int i = 0; i < numberOfPuyoNeeded; i++)
		{
			goalPuyoColor += ColorGenerator.GetColorFromHex(ColorGenerator.GetRandomColor());
		}

		return goalPuyoColor;

	}
	
	Vector3 generateGoalPuyoSize(int numberOfPuyoNeeded)
	{
		Vector3 goalPuyoSize = new Vector3();
		//int goalSize = Random.Range(minSize, maxSize + 1);
		double goalVolume = Math.Sqrt(numberOfPuyoNeeded);
		goalPuyoSize.Set((float) goalVolume, (float) goalVolume, (float) goalVolume);
		
		return goalPuyoSize;
	}
}
