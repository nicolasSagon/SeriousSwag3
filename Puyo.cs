using SeriousSwag3.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Puyo : MonoBehaviour
{
	public Text debugDisplay;
	public bool isOnGround;
	public bool isSpecialPuyo;

	public char randomKey;

	public Color colorValue;
	public GameObject BlueFx;
	public GameObject GreenFx;
	public GameObject redFX;
	public GameObject Corps;

	private GameSoundManager _gameSoundManager;
    
    
	// Use this for initialization
	void Start ()
	{
		_gameSoundManager = FindObjectOfType<GameSoundManager>();
		randomKey = KeyGenerator.GetRandomKey();
		colorValue = ColorGenerator.GetColorFromHex(ColorGenerator.GetRandomColor());

		ChangeColor(colorValue);
		debugDisplay.text = randomKey.ToString();
	}

	public void ChangeColor(Color color)
	{
		colorValue = color;

		var spriteRanderer = Corps.GetComponent<SpriteRenderer> ();
		spriteRanderer.color = color;
	}
    
	// Update is called once per frame
	void Update () {
		if (isOnGround || isSpecialPuyo)
		{
			debugDisplay.text = "";
		}
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

		_gameSoundManager.callDeathSound();
		
		Destroy(fxToDestroy, 2);
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Ground"))
		{
			_gameSoundManager.callGroundSound();
			isOnGround = true;    
		}
	}
}