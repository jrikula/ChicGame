using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
	public static BGM BackGroundMusic;

	private void Awake()
	{
		//so that themusic does't stop once we switch scenes
		DontDestroyOnLoad(this.gameObject);

		//so that the music isn't duplicated if we return to the menu scene
		if(BackGroundMusic == null)
		{
			BackGroundMusic = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
