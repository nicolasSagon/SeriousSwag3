using SeriousSwag3.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Puyo : MonoBehaviour
{

	public Material Material;
	public Text debugDisplay;
	public bool isOnGround;

	public char randomKey;

	public Color colorValue;
	
	
	// Use this for initialization
	void Start ()
	{
		randomKey = KeyGenerator.GetRandomKey();
		colorValue = ColorGenerator.GetColorFromHex(ColorGenerator.GetRandomColor());
		
		Material.color = colorValue;
		debugDisplay.text = randomKey.ToString();
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider ground)
	{
		isOnGround = true;
	}
}
