using UnityEngine;

public class GameManager : MonoBehaviour
{

	private BlobSpawner blobSpawner;
	
	// Use this for initialization
	void Start ()
	{
		blobSpawner = GetComponent<BlobSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
		
		foreach (char c in Input.inputString) {
			if (Input.anyKeyDown && c >='a' && c <='z')
			{
				var puyoList = blobSpawner.GetPuyoList();
				if (puyoList != null)
				{
					var puyo = puyoList.Find(e =>
					{
						var p = e.GetComponent<Puyo>();
						return p.randomKey == c;
					});

					if (puyo != null)
					{
						Destroy(puyo);
						puyoList.Remove(puyo);
					}
				}
			}
		}
	}
}
