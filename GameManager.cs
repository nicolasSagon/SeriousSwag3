using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject TimerGameObject;
    public int secondBeforeStartGame = 5;
    public int secondBeforeGameStop = 15;
    public GameObject Menu;
    public Button RetryButton;
    public Button QuitButton;
    public GameObject gaolpuyoGameobject;
    public GameObject goalSpawn;
    public GameObject resultSpawn;
    public GameObject timer;
         
    private BlobSpawner blobSpawner;
    private GameState currentState = GameState.INIT;
    private bool isStart = false;
    private bool isStop = false;
    private bool isCompletlyStop = false;
    private float _time;
    private Color _colorMerged;
    private Color _colorGoal;
    private GameObject _mergedPuyo;
    private GameObject _goalPuyo;
	private GameSoundManager _gameSoundManager;
    private Text timerDisplay;
	
    enum GameState
    {
        INIT,
        INIT_COUNTDOWN,
        START,
        FINISHED,
        END,
        SCORE,
        TRUE_END
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Use this for initialization
    void Start ()
    {
		_gameSoundManager = FindObjectOfType<GameSoundManager>();
        blobSpawner = GetComponent<BlobSpawner>();
        RetryButton.onClick.AddListener(retryGame);
        QuitButton.onClick.AddListener(returnToMenu);

        timerDisplay = TimerGameObject.GetComponent<Text>();
        TimerGameObject.SetActive(false);
        
        Menu.SetActive(false);
        
    }
    private void retryGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    private void returnToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    private void chooseGoalPuyoAndSpawn()
    {
        var goalPuyoFactory = new GoalPuyo(Instantiate(gaolpuyoGameobject));
        _goalPuyo = goalPuyoFactory.GetGoalPuyo();
        _colorGoal = goalPuyoFactory.color;
        _goalPuyo.transform.position = goalSpawn.transform.localPosition;
        currentState = GameState.INIT_COUNTDOWN;
    }
    
    // Update is called once per frame
    void Update () {
        if (_mergedPuyo != null)
        {
            var puyoScript = _mergedPuyo.GetComponent<Puyo>();
            puyoScript.ChangeColor(_colorMerged);
            puyoScript.isSpecialPuyo = true;
        }
        if (_goalPuyo != null)
        {
            var puyoScript = _goalPuyo.GetComponent<Puyo>();
            puyoScript.ChangeColor(_colorGoal);
            puyoScript.isSpecialPuyo = true;
        }
        switch (currentState)
        {
            case GameState.INIT:
                chooseGoalPuyoAndSpawn();
                break;
            case GameState.INIT_COUNTDOWN:
                var animator = timer.GetComponent<Animator>();
                if (!isStart)
                {
                    animator.SetBool("isAllowedToStart", true);
                    isStart = true;
                }
                else
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("play"))
                    {
                        Invoke("startGame", 1);
                    }
                }
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
                displayScore();
                currentState = GameState.TRUE_END;
                break;
            case GameState.TRUE_END:
                Menu.SetActive(true);
                break;
        }
    }
	
    private void displayScore()
	{
		var score = ScoreCalculator.ScoreCalc(_goalPuyo, _mergedPuyo);
		if (score < 50)
		{
			_gameSoundManager.callLooseSound();
		}
		else
		{
			_gameSoundManager.callWinSound();
		}
		displayMessage(score + "%");
	}
	
    void startGame()
    {
        blobSpawner.isStart = true;
        currentState = GameState.START;
        TimerGameObject.SetActive(true);
    }
    void stopGame()
    {
        isCompletlyStop = true;
        mergePuyo(blobSpawner.GetPuyoList());
        _mergedPuyo.transform.position = resultSpawn.transform.localPosition;
        var rigidbody2D = _mergedPuyo.GetComponent<Rigidbody2D>();
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        
        clearPuyoList(blobSpawner.GetPuyoList());
    }
    void displayMessage(string message)
    {
        timerDisplay.text = message;
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
                displayMessage("");
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
        _mergedPuyo = Instantiate(gaolpuyoGameobject);
        _mergedPuyo.transform.localScale = mergeSize(puyoOnGroundList);
        _colorMerged = mergeColor(puyoOnGroundList);
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
        
        mergedPuyoSize.x = Mathf.Sqrt(mergedPuyoSize.x);
        mergedPuyoSize.y = Mathf.Sqrt(mergedPuyoSize.y);
        mergedPuyoSize.z = Mathf.Sqrt(mergedPuyoSize.z);
        
        return mergedPuyoSize;
    }
    private void clearPuyoList(List<GameObject> puyoList)
    {
        puyoList.ForEach(Destroy);
        puyoList.Clear();
    }
}