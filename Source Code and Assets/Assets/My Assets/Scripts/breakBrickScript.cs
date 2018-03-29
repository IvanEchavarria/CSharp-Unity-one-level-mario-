using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakBrickScript : MonoBehaviour {

	[SerializeField] GameObject breakAnimation;
	[SerializeField] Animator anim;

	AudioSource aud;

	void Start () 
	{		
		aud = this.gameObject.GetComponent<AudioSource> ();
	}
		
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player" ) 
		{
			Vector2 direction = (other.transform.position - this.transform.position).normalized;

			if (Vector2.Angle (direction, Vector2.down) < 30.0f)
			{
				aud.Play ();
				anim.SetBool("isBroken", true);
				GameManager.destroyedObjs.Add (this.gameObject.name);
				Destroy (this.gameObject, 0.1f);
				GameObject piecesInst = Instantiate (breakAnimation, new Vector3 (transform.position.x + 0.03f , transform.position.y + 0.27f, 0f), transform.rotation);
				Destroy (piecesInst, 0.7f);

			}

		}

	}
}
