using System;
using System.Collections.Generic;
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
	private Color _color;
	private GameObject _mergedPuyo;

	enum GameState
	{
		INIT,
		START,
		FINISHED,
		END
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
				currentState = GameState.END;
				Invoke("stopGame", 4);
				break;
			case GameState.END:
				if (_mergedPuyo != null)
				{
					_mergedPuyo.GetComponent<Puyo>().ChangeColor(_color);	
				}
				break;
		}
	}

	void startGame()
	{
		blobSpawner.isStart = true;
		currentState = GameState.START;
	}

	void stopGame()
	{
		var puyoToInstance = mergePuyo(blobSpawner.getOnGroundList());
		puyoToInstance.transform.position = new Vector3(9, -3, 0);
		var rigidbody2D = puyoToInstance.GetComponent<Rigidbody2D>();
		rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
		
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
	
	GameObject mergePuyo(List<GameObject> puyoOnGroundList)
	{
		_mergedPuyo = Instantiate(blobSpawner.puyoGameObject);
		_mergedPuyo.transform.localScale = mergeSize(puyoOnGroundList);
		_color = mergeColor(puyoOnGroundList);

		return _mergedPuyo;
	}
	
	Color mergeColor(List<GameObject> puyoOnGroundList)
	{
		Color mergedPuyoColor = new Color();
		foreach (var puyo in puyoOnGroundList)
		{
			mergedPuyoColor += puyo.GetComponent<Puyo>().colorValue;
		}

		mergedPuyoColor /= puyoOnGroundList.Count;
		
		return mergedPuyoColor;
	}
	
	Vector3 mergeSize(List<GameObject> puyoOnGroundList)
	{
		Vector3 mergedPuyoSize = new Vector3();
		foreach (var puyo in puyoOnGroundList)
		{
			mergedPuyoSize += puyo.transform.localScale;
		}
		
		return mergedPuyoSize;
	}
}
