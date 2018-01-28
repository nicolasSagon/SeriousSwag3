using UnityEngine;

public class GameSoundManager : MonoBehaviour
{

	public AudioClip DeathSound;
	public AudioClip GroundHit;
	public AudioClip WinSound;
	public AudioClip LooseSound;
	public AudioClip MixerSound;


	private AudioSource _audioSource;
	
	// Use this for initialization
	void Start ()
	{
		_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void callDeathSound()
	{
		_audioSource.PlayOneShot(DeathSound);
	}

	public void callGroundSound()
	{
		_audioSource.PlayOneShot(GroundHit);
	}

	public void callWinSound()
	{
		_audioSource.PlayOneShot(WinSound);
	}

	public void callLooseSound()
	{
		_audioSource.PlayOneShot(LooseSound);
	}

	public void callMixerSound()
	{
		_audioSource.PlayOneShot(MixerSound);
	}
}
