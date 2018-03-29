using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformDownScript : MonoBehaviour {

	[SerializeField] float speed;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down * speed * Time.deltaTime);

		if(transform.position.y <= -1.195f )
		{
			transform.localPosition = new Vector3(transform.position.x,1.18f,0f);
		}
	}		
}
