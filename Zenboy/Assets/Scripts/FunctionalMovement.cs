using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionalMovement : MonoBehaviour {

	public float xMultipllier;
	public float speed;


	void Update () {
		transform.position =  new Vector2 (transform.position.x + (1f * speed * Time.deltaTime), Mathf.Sin (transform.position.x * xMultipllier));
	}
		
	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}
