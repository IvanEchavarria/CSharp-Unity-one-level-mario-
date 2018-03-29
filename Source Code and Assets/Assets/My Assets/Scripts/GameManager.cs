using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager instance = null;
	private static int lives = 3;
	private static int coinCount = 0;
	private static int timer = 300;
	public  Text livesText;
	public  Text coinsText;
	public  Text timerText;

	public static List<string> destroyedObjs = new List<string> ();

	private GameManager()
	{
		Debug.Log ("Game Manager Created");
	}

	public static GameManager Instance 
	{
		get 
		{
			if (instance == null) 
			{
				instance = new GameManager ();
			}
			return instance;
		}

	}
	// Use this for initialization
	void Start () {

		if (destroyedObjs.Count > 0) 
		{
			for (int i = 0; i < destroyedObjs.Count; i++)
			{
				Destroy (GameObject.Find (destroyedObjs[i]));
			}
			 
		}

		InvokeRepeating ("decrementTimer", 0f, 1f);
		
	}
	
	// Update is called once per frame
	void Update () {

		livesText.text = "Lives " + lives;
		coinsText.text = "X " + coinCount;
		timerText.text = "Time " + timer;

	}

	public static void loseLive()
	{
		lives--;
	}

	public static void addCoin()
	{
		coinCount++;
	}

	public static int returnLives()
	{
		return lives;
	}

	public static int returnTime()
	{
		return timer;
	}

	public static void setTime()
	{
		timer = 300;
	}


	void decrementTimer()
	{
		timer--;
	}
		
}
