using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomPuyo : MonoBehaviour {
	public Sprite[] corpsS;
	public Sprite[] outsideS;
	public Sprite[] YeuxS;
	public GameObject outside;
	public GameObject Corps;
	public GameObject Yeux;
	int IntRandom;
	int Intoutside;
	int IntYeux;

	// Use this for initialization
	void Start () {
		/*Corps = GameObject.Find ("Corps");
		outside = GameObject.Find ("outside");
		Yeux = GameObject.Find ("Yeux");*/
		Intoutside = Random.Range(0, 5);
		IntRandom = Random.Range(0, 6);
		IntYeux = Random.Range (0,6);
		Corps.GetComponent<SpriteRenderer>().sprite = corpsS[IntRandom];
		outside.GetComponent<SpriteRenderer>().sprite = outsideS[Intoutside];
		Yeux.GetComponent<SpriteRenderer>().sprite = YeuxS[IntYeux];
	}
	
	// Update is called once per frame
	void Update () {
		if (Intoutside == 2) {
			outside.transform.localPosition = new Vector3 (0f, 0.1f, 0f);			
		} else {
			outside.transform.localPosition = new Vector3 (0f, 0f, 0f);
		}
	}
}
