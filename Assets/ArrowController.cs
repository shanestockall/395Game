using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	float speed; 
	float lifetime;
	float startTime; 

	// Use this for initialization
	void Start () {
		speed = 5f; 
		lifetime = 5f; 
		startTime = Time.time; 

	}
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.forward * speed * Time.deltaTime;

		if (Time.time > startTime + lifetime) 
			Destroy (transform.gameObject); 

	}

	private void OnCollisionEnter2D (Collision2D other) { 

		if (other.gameObject.tag == "enemy") { 
			other.gameObject.GetComponent<EnemyController> ().health -= GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().strength.value;
		}

		Destroy (transform.gameObject); 
	} 
}
