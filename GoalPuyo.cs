using System;
using System.Collections;
using System.Collections.Generic;
using SeriousSwag3.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoalPuyo{

	public GameObject goalPuyo;
	public int minSizeSquare = 4;
	public int maxSizeSquare = 16;
	
	public GameObject input;
	public Color color;

	public GoalPuyo(GameObject inputGameObject)
	{
		goalPuyo = inputGameObject;
	}

	public GameObject GetGoalPuyo()
	{
		generateGoalPuyo();
		return goalPuyo;
	}

	void generateGoalPuyo()
	{
		int numberOfPuyoNeeded = Random.Range(minSizeSquare, maxSizeSquare + 1);
		Debug.Log(numberOfPuyoNeeded);
		goalPuyo.transform.localScale = generateGoalPuyoSize(numberOfPuyoNeeded);
		color = generateGoalPuyoColor(numberOfPuyoNeeded);
		var puyoScript = goalPuyo.GetComponent<Puyo>();
		puyoScript.ChangeColor(color);
		puyoScript.isSpecialPuyo = true;
	}
	
	Color generateGoalPuyoColor(int numberOfPuyoNeeded)
	{
		Color goalPuyoColor = new Color();

		goalPuyoColor = ColorGenerator.GetColorFromHex(ColorGenerator.GetRandomColor());

		for (int i = 0; i < numberOfPuyoNeeded; i++)
		{
			goalPuyoColor += ColorGenerator.GetColorFromHex(ColorGenerator.GetRandomColor());
		}

		goalPuyoColor /= numberOfPuyoNeeded;
		
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
