using UnityEngine;
using System.Collections;

public class BaymaxMovement : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}

	public float moveSpeed = 30f;
	public float turnSpeed = 50f;

	void moveForward ()
	{
		Vector3 vect = new Vector3 (-1, 0, 0);
		transform.Translate (vect * moveSpeed * Time.deltaTime);
	}

	void moveBackwards ()
	{
		Vector3 vect = new Vector3 (1, 0, 0);
		transform.Translate (vect * moveSpeed * Time.deltaTime);
	}

	void rotateLeft ()
	{
		transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);
	}

	void rotateRight ()
	{
		transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
	}
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKey (KeyCode.UpArrow)) {
			moveForward ();
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			moveBackwards ();
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			rotateLeft ();
		}
            

		if (Input.GetKey (KeyCode.RightArrow)) {
			rotateRight ();
		}
		if (Input.GetKeyUp (KeyCode.J))
			transform.Translate (new Vector3 (0, 1, 0));
	}

	void OnCollisionEnter (Collision collision)
	{
		Debug.Log ("Enter called.");
	}
}
