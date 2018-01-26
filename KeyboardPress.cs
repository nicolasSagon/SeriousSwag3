using UnityEngine;

public class KeyboardPress : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (char c in Input.inputString) {
			if (Input.anyKeyDown && c >='a' && c <='z') {
				Debug.Log(c);
			}
		}
	}
}
