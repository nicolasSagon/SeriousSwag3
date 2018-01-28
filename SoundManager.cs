using UnityEngine;

public class SoundManager : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}
