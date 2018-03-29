using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pipeEntrance : MonoBehaviour {

	[SerializeField] float loadDelay;
	[SerializeField] string sceneToLoad;
	bool playSoundOnce;
	Scene scene;
	AudioSource aud;

	// Use this for initialization
	void Start () {

		scene = SceneManager.GetActiveScene ();
		aud = this.gameObject.GetComponent<AudioSource> ();
		playSoundOnce = true;
	}




	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player" && this.tag == "lastScenePipeHorizontal") 
		{
			Vector2 direction = (other.transform.position - this.transform.position).normalized;

			if (Vector2.Angle (direction, Vector2.left) < 70.0f)
			{
				if (playSoundOnce) 
				{
					playSoundOnce = false;
					aud.Play ();
				}

				if (scene.name == "Bonus") 
				{
					PlayerScript.setMarioPosition();
				}
				Invoke ("LoadScene", loadDelay);
			}

		}

		if (other.gameObject.tag == "Player" && this.tag == "lastScenePipeV" ) 
		{
			Vector2 direction = (other.transform.position - this.transform.position).normalized;

			/*Debug.DrawRay (this.transform.position, direction, Color.red);

			Debug.DrawRay (this.transform.position, Vector2.up, Color.green);

			Debug.Log ("Pipe Angle: " +Vector2.Angle (direction, Vector2.up));*/

			if ((Vector2.Angle (direction, Vector2.up) < 24.5f && (Vector2.Angle (direction, Vector2.up) > 0.0f)) && Input.GetKeyDown (KeyCode.S))
			{
				if (playSoundOnce) 
				{ 
					playSoundOnce = false;
					aud.Play ();
				}
				GameManager.destroyedObjs.Add (this.gameObject.name);
				Invoke ("LoadScene", loadDelay);
			}

		}

  	}

	void LoadScene()
	{		
		
		SceneManager.LoadScene (sceneToLoad);

	}
}