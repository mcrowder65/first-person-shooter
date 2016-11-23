using UnityEngine;
using UnityEngine.Networking;

public class BaymaxMovement : NetworkBehaviour
{

	
	public float moveSpeed = 30f;
	public float turnSpeed = 50f;
	public float speedH = 2.0f;
	private float yaw = -1f;
	// Use this for initialization
	void Start ()
	{
	}


	void moveForwardsOrBackwards (float val)
	{
		Vector3 vect = new Vector3 (val, 0, 0);
		transform.Translate (vect * moveSpeed * Time.deltaTime);
	}

	void moveLeftOrRight (float val)
	{
		Vector3 vect = new Vector3 (0, 0, val);
		transform.Translate (vect * moveSpeed * Time.deltaTime);
	}

	public float speedV = 2.0f;

	private float pitch = -1f;

	void rotateVertically ()
	{
		Camera cam = GetComponentInChildren<Camera> ();
		//TODO figure out how to get this working only for camera :( 
		if (pitch == -1f) {
			pitch = cam.transform.eulerAngles.x;
		}
		pitch -= speedV * Input.GetAxis (Utilities.isXboxController () ? "Right joystick vertical" : "Mouse Y");

		cam.transform.eulerAngles = new Vector3 (pitch, cam.transform.eulerAngles.y, 0.0f);
		// Clamp pitch:
		pitch = Mathf.Clamp (pitch, -90f, 90f);

		cam.transform.eulerAngles = new Vector3 (pitch, cam.transform.eulerAngles.y, 0f);
	}

	void rotateHorizontally ()
	{
		if (yaw == -1f) {
			yaw = transform.eulerAngles.y;
		}
        
		yaw += speedH * Input.GetAxis (Utilities.isXboxController () ? "Right joystick horizontal" : "Mouse X");
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, yaw, 0.0f);

		// Wrap yaw:
		while (yaw < 0f) {
			yaw += 360f;
		}
		while (yaw >= 360f) {
			yaw -= 360f;
		}
		// Set orientation:
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, yaw, 0f);
	}

	void jump ()
	{
		transform.Translate (new Vector3 (0, 1, 0));
	}
    
	// Update is called once per frame
	void Update ()
	{
		
		if (!isLocalPlayer) {
			GetComponentInChildren<Camera> ().enabled = false;
			return;
		}
		rotateHorizontally ();
		rotateVertically ();
		if (Utilities.isXboxController ()) {
			moveLeftOrRight (Input.GetAxis ("Left joystick left right"));
			moveForwardsOrBackwards (Input.GetAxis ("Left joystick forwards backwards"));
			if (Input.GetKeyUp (KeyCode.JoystickButton0)) {
				jump ();
			}
		} else {
			if (Input.GetKey (KeyCode.A)) {
				moveLeftOrRight (-1);
			}
			if (Input.GetKey (KeyCode.D)) {
				moveLeftOrRight (1);
			}
			if (Input.GetKey (KeyCode.W)) {
				moveForwardsOrBackwards (-1);
			}
			if (Input.GetKey (KeyCode.S)) {
				moveForwardsOrBackwards (1);
			}
			if (Input.GetKeyUp (KeyCode.J)) {
				jump ();
			}
		}
		
		
			

	}

}
