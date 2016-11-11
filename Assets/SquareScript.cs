using UnityEngine;
using System.Collections;

public class SquareScript : MonoBehaviour {

	void Start() 
	{
		
	}
//	void Update()
//	{
//		if (Input.GetKeyDown (KeyCode.D)) {
//			transform.Rotate(Vector3.right * Time.deltaTime);
//		}
//		if (Input.GetKeyDown (KeyCode.A)) {
//			transform.Rotate(Vector3.left * Time.deltaTime);
//		}
//	}
//	void FixedUpdate()
//	{
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");
//
//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
//		transform.Translate(movement);
//	}
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;


	void Update ()
	{
		if(Input.GetKey(KeyCode.UpArrow))
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.DownArrow))
			transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
		if (Input.GetKeyUp (KeyCode.J))
			transform.Translate (new Vector3 (0, 1, 0));
	}
}
