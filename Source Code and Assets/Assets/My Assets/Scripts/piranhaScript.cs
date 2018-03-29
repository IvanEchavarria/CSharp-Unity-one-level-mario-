using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piranhaScript : MonoBehaviour 
{
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "fireBall") 
		{
			GameManager.destroyedObjs.Add (this.gameObject.name);
			Destroy (this.gameObject, 0.1f);
		}

	}


}
