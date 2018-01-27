using System.Collections;
using System.Collections.Generic;
using SeriousSwag3.Utils;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour {

	public GameObject input;
	
	// Use this for initialization
	void Start () {
		GameObject puyo1 = Instantiate(input);
		GameObject puyo2 = Instantiate(input);
		
		puyo1.GetComponent<Puyo>().colorValue = ColorGenerator.GetColorFromHex("#66f2ae");
		puyo2.GetComponent<Puyo>().colorValue = ColorGenerator.GetColorFromHex("#51C18B");
		puyo1.transform.localScale=Vector3.one*5;
		puyo2.transform.localScale = Vector3.one*3;

		int score = scoreCalc(puyo1, puyo2);
		Debug.Log("score = " + score + "%");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int scoreCalc(GameObject goalPuyo, GameObject mergedPuyo)
	{
		int score = 0;
		float colorDelta = colorDeltaCalc(goalPuyo.GetComponent<Puyo>().colorValue, mergedPuyo.GetComponent<Puyo>().colorValue);
		float sizeDelta = sizeDeltaCalc(goalPuyo.transform.localScale, mergedPuyo.transform.localScale);
		float scoreSeed = (3f - colorDelta) - 2*sizeDelta;
		// plus scoreSeed est grand, plus les 2 puyo sont proches

		if (scoreSeed > 0)
			score = (int)Mathf.Round(scoreSeed * 100) / 3;
		
		return score;
	}

	float colorDeltaCalc(Color c1, Color c2)
	{
		float rDelta = System.Math.Abs(c1.r - c2.r);
		float gDelta = System.Math.Abs(c1.g - c2.g);
		float bDelta = System.Math.Abs(c1.b - c2.b);
		float colorDelta = rDelta + gDelta + bDelta;
		// vaut entre 0 et 3
		// 0 = très bien
		// 3 = très nul

		return colorDelta;
	}
	
	float sizeDeltaCalc(Vector3 v1, Vector3 v2)
	{
		float sizeDelta = 1 - Mathf.Min(v1.x, v2.x)/Mathf.Max(v1.x, v2.x);
		Debug.Log("sizeDelta = " + sizeDelta);
		return sizeDelta;
	}
}
