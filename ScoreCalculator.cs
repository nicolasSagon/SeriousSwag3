using System;
using SeriousSwag3.Utils;
using UnityEngine;

public class ScoreCalculator {

	public static int ScoreCalc(GameObject goalPuyo, GameObject mergedPuyo)
	{
		int score = 0;
		float colorDelta = colorDeltaCalc(goalPuyo.GetComponent<Puyo>().colorValue, mergedPuyo.GetComponent<Puyo>().colorValue);
		float sizeDelta = sizeDeltaCalc(goalPuyo.transform.localScale, mergedPuyo.transform.localScale);
		float scoreSeed = (3f - colorDelta) - 2*sizeDelta;

		if (scoreSeed > 0)
			score = (int)Mathf.Round(scoreSeed * 100) / 3;
		
		return score;
	}

	private static float colorDeltaCalc(Color c1, Color c2)
	{
		float rDelta = Math.Abs(c1.r - c2.r);
		float gDelta = Math.Abs(c1.g - c2.g);
		float bDelta = Math.Abs(c1.b - c2.b);
		float colorDelta = rDelta + gDelta + bDelta;
		// vaut entre 0 et 3
		// 0 = très bien
		// 3 = très nul

		return colorDelta;
	}
	
	private static float sizeDeltaCalc(Vector3 v1, Vector3 v2)
	{
		float sizeDelta = 1 - Mathf.Min(v1.x, v2.x)/Mathf.Max(v1.x, v2.x);
		return sizeDelta;
	}
}
