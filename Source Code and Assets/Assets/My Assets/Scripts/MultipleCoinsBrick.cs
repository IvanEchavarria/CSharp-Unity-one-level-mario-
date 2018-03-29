using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleCoinsBrick : MonoBehaviour {
	
	[SerializeField] GameObject coinPrefab;
	[SerializeField] Animator anim;
	int numberCoins;
	int coinsLimit;
	float YPosition;

	AudioSource aud;

	// Use this for initialization
	void Start () {

		aud = this.gameObject.GetComponent<AudioSource> ();
		coinsLimit = Random.Range (10, 15);
		numberCoins = 0;
		YPosition = this.transform.position.y;
	}

	void Update () 
	{
		if (this.transform.position.y > YPosition) 
		{		
			transform.localPosition = new Vector3(transform.position.x,transform.position.y - 0.02f,0f);
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player" ) 
		{
			Vector2 direction = (other.transform.position - this.transform.position).normalized;

			if (Vector2.Angle (direction, Vector2.down) < 30.0f)
			{
				aud.Play ();

				if (numberCoins <= coinsLimit) {

					transform.localPosition = new Vector3(transform.position.x,transform.position.y + 0.2f,0f);
					GameObject coinInst = Instantiate (coinPrefab, new Vector3 (transform.position.x, transform.position.y + 0.17f, 0f), transform.rotation);
					Destroy (coinInst, 0.2f);
					GameManager.addCoin ();
					numberCoins++;
				} 

				if(numberCoins > coinsLimit)
				{
					anim.SetBool("noMoreCoins", true);
				}

			}

		}

	}
}
