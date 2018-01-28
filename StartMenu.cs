using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

	public Button startButton;
	public Button quitButton;
	private AudioSource audioSource;
	
	// Use this for initialization
	void Start () {
		startButton.onClick.AddListener(startGame);
		quitButton.onClick.AddListener(quitGame);

		audioSource = GetComponent<AudioSource>();
	}


	void startGame()
	{
		audioSource.Play();
		Invoke("loadScene", 1);
	}

	private void loadScene()
	{
		SceneManager.LoadScene("Playtest1", LoadSceneMode.Single);
	}

	void quitGame()
	{
		Application.Quit();
	}
	
}
