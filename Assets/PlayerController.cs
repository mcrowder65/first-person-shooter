using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float moveSpeed = 30f;
	public float turnSpeed = 50f;
	public float speedH = 2.0f;
	private float yaw = -1f;
	private float previousXAxis = 0f;
	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	void CmdFire ()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (
			             bulletPrefab,
			             bulletSpawn.position,
			             bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn (bullet);

		// Destroy the bullet after 2 seconds
		Destroy (bullet, 2.0f);
	}

	public override void OnStartLocalPlayer ()
	{
		GetComponent<MeshRenderer> ().material.color = Color.blue;
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
		if (!networkView.isMine) {
			camera.enabled = false;
		}

        
		if (!isLocalPlayer) {
			return;
		}
		rotateHorizontally ();

		if (Utilities.isXboxController ()) {
			moveLeftOrRight (-Input.GetAxis ("Left joystick forwards backwards"));
			moveForwardsOrBackwards (Input.GetAxis ("Left joystick left right"));
			if (Input.GetKeyUp (KeyCode.JoystickButton0)) {
				jump ();
			}
			float triggerInput = Input.GetAxis ("Triggers");
			//Debug.Log(triggerInput);
			if (triggerInput > 0) { //left trigger
				Debug.Log ("Grenade!!!");
			} else if (triggerInput < 0) { //right trigger
				CmdFire ();
			}
		} else {
			if (Input.GetKey (KeyCode.A)) {
				moveForwardsOrBackwards (-1);
			}
			if (Input.GetKey (KeyCode.D)) {
				moveForwardsOrBackwards (1);
			}
			if (Input.GetKey (KeyCode.W)) {
				moveLeftOrRight (1);
                
			}
			if (Input.GetKey (KeyCode.S)) {
				moveLeftOrRight (-1);
			}
			if (Input.GetKeyUp (KeyCode.J)) {
				jump ();
			}
			if (Input.GetKey (KeyCode.Space)) {
				CmdFire ();
			}
		}




	}
}