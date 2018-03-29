using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour {

	[SerializeField] GameObject coinPrefab;
	[SerializeField] Animator anim;
	float numberCoins = 0;
	float thisYPosition;
	AudioSource aud;

	// Use this for initialization
	void Start () {

		aud = this.gameObject.GetComponent<AudioSource> ();
		thisYPosition = this.transform.position.y;
	}

	void Update () 
	{
		if (this.transform.position.y > thisYPosition) 
		{		
			transform.localPosition = new Vector3(transform.position.x,transform.position.y - 0.05f,0f);
		}

	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Vector2 direction = (other.transform.position - this.transform.position).normalized;

			//Debug.DrawRay (this.transform.position, direction, Color.red);

			//Debug.DrawRay (this.transform.position, Vector2.down, Color.green);

			//Debug.Log (Vector2.Angle (direction, Vector2.down));

			if (Vector2.Angle (direction, Vector2.down) < 30.0f)
			{
				aud.Play ();
				if (numberCoins < 1) 
				{
					transform.localPosition = new Vector3(transform.position.x,transform.position.y + 0.2f,0f);
					GameObject coinInst = Instantiate (coinPrefab, new Vector3 (transform.position.x, transform.position.y + 0.20f, 0f), transform.rotation);
					Destroy (coinInst, 0.2f);
					anim.SetBool("hasCoin", false);
					GameManager.addCoin ();
					numberCoins++;
				}

			}

		}

	}

	void changeCoins()
	{
		numberCoins = 2;
		anim.SetBool("hasCoin", false);
	}


}
