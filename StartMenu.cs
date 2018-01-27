using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

	public Button startButton;
	public Button quitButton;
	public AudioClip clickSound;
	private AudioSource source;
	
	// Use this for initialization
	void Start () {
		startButton.onClick.AddListener(startGame);
		quitButton.onClick.AddListener(quitGame);
		
		source = GetComponent<AudioSource>();
		source.clip = clickSound;
	}


	void startGame()
	{
		SceneManager.LoadScene("Playtest1", LoadSceneMode.Single);
	}

	void quitGame()
	{
		Application.Quit();
	}
	
	void TaskOnClick()
	{
		source.Play();
		
	}
}
