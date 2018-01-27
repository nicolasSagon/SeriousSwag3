using SeriousSwag3.Utils;
using UnityEngine;
using UnityEngine.UI;

public class textColorSwapper : MonoBehaviour
{

	public float timeBetweenColorSwap = 5;
	
	private float timeLeftBeforeChangeColor;
	
	// Use this for initialization
	void Start () {
		timeLeftBeforeChangeColor = timeBetweenColorSwap;
	}
	
	// Update is called once per frame
	void Update () {
		
		timeLeftBeforeChangeColor -= Time.deltaTime;
		if (timeLeftBeforeChangeColor <= 0)
		{
			changeTextColor();
			timeLeftBeforeChangeColor = timeBetweenColorSwap;
		}
	}

	private void changeTextColor()
	{
		var text = GetComponent<Text>();
		text.color = ColorGenerator.GetColorFromHex(ColorGenerator.GetRandomColor());
	}
}
