using UnityEngine;

public class RandomPuyo : MonoBehaviour {
	public Sprite[] YeuxS;
	public GameObject Yeux;
	int IntRandom;
	int IntYeux;

	private Vector3[] yeuxPosition = {
		new Vector3(0.00f,0,0),
		new Vector3(0.00f,-0.25f,0),
		new Vector3(0.00f,0.25f,0)
	};

	// Use this for initialization
	void Start () {

		IntYeux = Random.Range (0,YeuxS.Length);
		Yeux.GetComponent<SpriteRenderer>().sprite = YeuxS[IntYeux];

		var eyePosition = Random.Range (0, yeuxPosition.Length);
		Yeux.transform.localPosition = yeuxPosition [eyePosition];
	}
}