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
	private bool isCompletlyStop = false;
	private float _time;
	private Color _color;
	private GameObject _mergedPuyo;
	private GameObject _goalPuyo;

	enum GameState
	{
		INIT,
		INIT_COUNTDOWN,
		START,
		FINISHED,
		END,
		SCORE
	}

	private void Awake()
	{
		Application.targetFrameRate = 40;
	}

	// Use this for initialization
	void Start ()
	{
		blobSpawner = GetComponent<BlobSpawner>();	
	}

	private void chooseGoalPuyoAndSpawn()
	{
		var goalPuyoFactory = new GoalPuyo(Instantiate(blobSpawner.puyoGameObject));
		_goalPuyo = goalPuyoFactory.GetGoalPuyo();
		_goalPuyo.transform.position = new Vector3(-9, -2, 0);
		currentState = GameState.INIT_COUNTDOWN;
	}
	
	// Update is called once per frame
	void Update () {
		if (_mergedPuyo != null)
		{
			_mergedPuyo.GetComponent<Puyo>().ChangeColor(_color);
		}
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
				chooseGoalPuyoAndSpawn();
				break;
			case GameState.INIT_COUNTDOWN:
				break;
			case GameState.START:
				checkKeyboard();
				updateGameTimer();
				break;
			case GameState.FINISHED:
				currentState = GameState.END;
				checkKeyboard();
				Invoke("stopGame", 4);
				break;
			case GameState.END:
				if (!isCompletlyStop)
				{
					checkKeyboard();
				}
				else
				{
					currentState = GameState.SCORE;
				}
				break;
			case GameState.SCORE:
				displayMessage("YOU HAVE WIN WITH 100% GG");
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
		isCompletlyStop = true;
		mergePuyo(blobSpawner.GetPuyoList());
		_mergedPuyo.transform.position = new Vector3(9, -3, 0);
		var rigidbody2D = _mergedPuyo.GetComponent<Rigidbody2D>();
		rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
		
		clearPuyoList(blobSpawner.GetPuyoList());
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
	
	void mergePuyo(List<GameObject> puyoOnGroundList)
	{
		_mergedPuyo = Instantiate(blobSpawner.puyoGameObject);
		_mergedPuyo.transform.localScale = mergeSize(puyoOnGroundList);
		_color = mergeColor(puyoOnGroundList);
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

	private void clearPuyoList(List<GameObject> puyoList)
	{
		puyoList.ForEach(Destroy);
		puyoList.Clear();
	}
}
