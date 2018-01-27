using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public Text TimerDisplay;
	public int secondBeforeStartGame = 5;
	public int secondBeforeGameStop = 15;
		 
	private BlobSpawner blobSpawner;

	private GameState currentState = GameState.INIT;
	private bool isStart = false;
	private bool isStop = false;
	private float _time;
	
	enum GameState
	{
		INIT,
		START,
		FINISHED
	}
	
	// Use this for initialization
	void Start ()
	{
		blobSpawner = GetComponent<BlobSpawner>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isStart)
		{
			_time += Time.deltaTime;
			displayMessage(Math.Truncate(secondBeforeStartGame - _time).ToString());
			if (_time >= secondBeforeStartGame)
			{
				isStart = true;
				_time = 0;
				displayMessage("START");
				Invoke("startGame", 2);
			}
		}
		switch (currentState)
		{
			case GameState.INIT:
				break;
			case GameState.START:
				checkKeyboard();
				updateGameTimer();
				break;
			case GameState.FINISHED:
				break;
		}
	}

	void startGame()
	{
		blobSpawner.isStart = true;
		currentState = GameState.START;
	}

	void displayMessage(string message)
	{
		TimerDisplay.text = message;
	}

	void updateGameTimer()
	{
		if (!isStop)
		{
			_time += Time.deltaTime;
			displayMessage(Math.Truncate(secondBeforeGameStop - _time).ToString());
			if (_time >= secondBeforeGameStop)
			{
				blobSpawner.isStart = false;
				currentState = GameState.FINISHED;
				displayMessage("END");
			}
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
}
