using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] float loadDelay;
	AudioSource aud;

	// Use this for initialization
	void Start () {

		GameManager.setTime ();
		GameManager.destroyedObjs.Clear ();
		aud = this.gameObject.GetComponent<AudioSource> ();
		aud.Play();
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * speed * Time.deltaTime);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ground") {

			speed = 0;
			Invoke ("LoadScene", loadDelay);

		}
	}

	void LoadScene()
	{
		SceneManager.LoadScene ("Main");

	}
}
