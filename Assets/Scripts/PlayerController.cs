using UnityEngine;
using System.Collections;
using System;
public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private int movespeed = 1;
	void Start() 
	{
		rb = GetComponent<Rigidbody> ();
		Debug.Log ("Start!");
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.W)) {
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + movespeed);
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			transform.position = new Vector3(transform.position.x - movespeed, transform.position.y, transform.position.z);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			transform.position = new Vector3(transform.position.x + movespeed, transform.position.y, transform.position.z);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - movespeed);
		}
	}
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

//		rb.AddForce (movement);
			
	}
}
