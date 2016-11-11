using UnityEngine;
using System.Collections;

public class SquareScript : MonoBehaviour {

	private Rigidbody rb;
	private int movespeed = 1;
	void Start() 
	{
		rb = GetComponent<Rigidbody> ();
		Debug.Log ("Start!");
	}
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement);

	}
}
