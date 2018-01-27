using System.Collections;
using SeriousSwag3.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Puyo : MonoBehaviour
{
	public Text debugDisplay;
	public bool isOnGround;

	public char randomKey;

	public Color colorValue;
	public GameObject BlueFx;
	public GameObject GreenFx;
	public GameObject redFX;
	
	
	// Use this for initialization
	void Start ()
	{
		randomKey = KeyGenerator.GetRandomKey();
		colorValue = ColorGenerator.GetColorFromHex(ColorGenerator.GetRandomColor());

		ChangeColor(colorValue);
		debugDisplay.text = randomKey.ToString();
	}

	public void ChangeColor(Color color)
	{
		var randomPuyo = GetComponent<RandomPuyo>();
		var corp = randomPuyo.Corps;
		
		var rendCorp = corp.GetComponent<SpriteRenderer>();
		rendCorp.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnDestroy()
	{
		Vector3 position = transform.position;

		GameObject fxToDestroy;
		
		if (colorValue == Color.green)
		{
			fxToDestroy = Instantiate(GreenFx, position, Quaternion.identity);
		}
		else if (colorValue == Color.blue)
		{
			fxToDestroy = Instantiate(BlueFx, position, Quaternion.identity);
		}
		else
		{
			fxToDestroy = Instantiate(redFX, position, Quaternion.identity);
		}
		

		Destroy(fxToDestroy, 2);
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Ground"))
		{
			isOnGround = true;	
		}
	}
}
