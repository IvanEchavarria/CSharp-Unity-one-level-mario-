using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformUpScript : MonoBehaviour {

	[SerializeField] float speed;

	// Update is called once per frame
	void Update () {
		
		transform.Translate (Vector3.up * speed * Time.deltaTime);

		if(transform.position.y >= 1.18f)
		{
			transform.localPosition = new Vector3(transform.position.x, -1.195f ,0f);
		}
	}		
}
