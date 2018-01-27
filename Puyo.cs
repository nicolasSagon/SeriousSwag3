using SeriousSwag3.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Puyo : MonoBehaviour
{
	public Text debugDisplay;
	public bool isOnGround;

	public char randomKey;

	public Color colorValue;
	
	
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
		var rend = GetComponent<Renderer>();
		rend.material.SetColor("_BgColor", color);
		rend.material.SetColor("_BoundColor", color);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Ground"))
		{
			isOnGround = true;	
		}
	}
}
