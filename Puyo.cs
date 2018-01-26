using SeriousSwag3.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Puyo : MonoBehaviour
{

	public Material Material;
	public Text debugDisplay;
	private bool isOnGround;

	private char randomKey
	{
		get { return KeyGenerator.GetRandomKey(); }
	}

	private Color colorValue
	{
		get { return ColorGenerator.GetColorFromHex(ColorGenerator.GetRandomColor()); }
	}
	
	
	// Use this for initialization
	void Start ()
	{
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
