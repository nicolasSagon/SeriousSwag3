using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public Text TimerDisplay;
		 
	private BlobSpawner blobSpawner;

	private GameState currentState = GameState.START;
	
	// Use this for initialization
	void Start ()
	{
		blobSpawner = GetComponent<BlobSpawner>();	
	}

	IEnumerator StartGame()
	{
		blobSpawner.isStart = true;
		currentState = GameState.START;
		yield return new WaitForSeconds(10);
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState)
		{
			case GameState.INIT:
				break;
			case GameState.START:
				checkKeyboard();
				break;
			case GameState.FINISHED:
				break;
		}
	}


	void checkKeyboard()
	{
		foreach (char c in Input.inputString) {
			if (Input.anyKeyDown && c >='a' && c <='z')
			{
				removePuyo(c);
			}
		}
	}
	
	void removePuyo(char c)
	{
		var puyoList = blobSpawner.GetPuyoList();
		if (puyoList != null)
		{
			var puyo = puyoList.Find(e =>
			{
				var p = e.GetComponent<Puyo>();
				return p.randomKey == char.ToUpper(c) && !p.isOnGround;
			});

			if (puyo != null)
			{
				Destroy(puyo);
				puyoList.Remove(puyo);
			}
		}
	}

	enum GameState
	{
		INIT,
		START,
		FINISHED
	}
}
