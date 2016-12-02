using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public abstract class PlayerController : NetworkBehaviour
{


	public float moveSpeed = 30f;
	public float turnSpeed = 50f;
	public float speedH = 2.0f;
	private float yaw = -1f;
	public float speedV = 2.0f;

	public Transform bulletSpawn;


    //TODO: THIS IS TEMPORARY
    public GameObject SubmachinegunPrefab;
    public GameObject FlamethrowerPrefab;

    public Weapon currentWeapon;


    private GenericTimer jumpCooldownTimer = new GenericTimer(0.5f, false);
    private bool jumpIsCoolingDown = false;

	// Use this for initialization
	public void Start ()
	{
		updateScoreboard ();
		Physics.gravity = new Vector3 (0, -50, 0);

        //TODO: THIS IS TEMPORARY
        GameObject weapon = (GameObject) Instantiate(SubmachinegunPrefab, this.transform, false);
        currentWeapon = weapon.GetComponent<Weapon>();
	}
   

	public override void OnStartLocalPlayer ()
	{
		transform.position = Utilities.getNewRespawnPoint ();
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

	void CmdRotateVertically ()
	{

		Camera cam = GetComponentInChildren<Camera> ();
        //TODO: Generalize to work with any type of gun
		var gun = transform.Find ("SubmachineGun");
		if (currentWeapon.pitch == Constants.INVALID_PITCH) {
            currentWeapon.pitch = cam.transform.eulerAngles.x;
		}
        currentWeapon.pitch -= speedV * Input.GetAxis (Utilities.isXboxController () ? "Right joystick vertical" : "Mouse Y");

		cam.transform.eulerAngles = new Vector3 (currentWeapon.pitch, cam.transform.eulerAngles.y, 0.0f);
		gun.transform.eulerAngles = new Vector3 (gun.transform.eulerAngles.x, gun.transform.eulerAngles.y, currentWeapon.pitch);
        // Clamp pitch:
        currentWeapon.pitch = Mathf.Clamp (currentWeapon.pitch, -70f, 70f);

		cam.transform.eulerAngles = new Vector3 (currentWeapon.pitch, cam.transform.eulerAngles.y, 0f);
		gun.transform.eulerAngles = new Vector3 (gun.transform.eulerAngles.x, gun.transform.eulerAngles.y, currentWeapon.pitch);
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
		if (!jumpIsCoolingDown && IsOnGround ()) {
			GetComponent<Rigidbody> ().AddForce (Vector3.up * Constants.JUMP_FORCE);
            jumpCooldownTimer.Enabled = true;
            jumpIsCoolingDown = true;
		}
	}

	bool IsOnGround ()
	{
		float raycastDistance = GetComponent<Collider> ().bounds.extents.y + 0.4f;
		return Physics.Raycast (transform.position, -Vector3.up, raycastDistance);
	}

	bool deathByFalling ()
	{
		if (transform.position.y > Constants.DEATH_Y) {
			return false;
		}
		var health = GetComponent<Health> ();
		health.deathByFalling ();
		updateScoreboard ();
		return true;
	}

	[Command]
	public void CmdFire ()
	{
        if (!currentWeapon.CanFire)
            return;

		var canvas = transform.Find ("Canvas");
		var crossHair = canvas.Find ("Crosshair");
        // Create the Bullet from the Bullet Prefab

        Projectile projectile = currentWeapon.CreateProjectile(crossHair);
        NetworkServer.Spawn(projectile.gameObject);

        //var bullet = (GameObject)Instantiate (
        //	             bulletPrefab,
        //	             crossHair.position,
        //	             bulletSpawn.rotation,
        //	             transform);
        //// Add velocity to the bullet

        //bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 100;


        //      // Destroy the bullet after 2 seconds
        //      Destroy(bullet, 1.5f);

        // Spawn the bullet on the Clients
        //  NetworkServer.Spawn (bullet);

    }

	public void updateScoreboard ()
	{
		var canvas = transform.Find ("Canvas");
		var scoreboard = canvas.Find ("Scoreboard");
		var health = GetComponent<Health> ();
		scoreboard.GetComponent<UnityEngine.UI.Text> ().text = "Kills: " + health.kills + " Deaths: " + health.deaths;
	}

	void Update ()
	{

		if (!isLocalPlayer) {
			GetComponentInChildren<Camera> ().enabled = false;
			return;
		}
		if (deathByFalling ()) {
			return;
		}
		rotateHorizontally ();
		CmdRotateVertically ();
		if (Utilities.isXboxController ()) {
			moveLeftOrRight (Input.GetAxis ("Left joystick left right"));
			moveForwardsOrBackwards (Input.GetAxis ("Left joystick forwards backwards"));
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
			if (Input.GetKeyUp (KeyCode.Space)) {
				jump ();
			}
			if (Input.GetKey (KeyCode.Mouse0)) {
				CmdFire ();
			}
		}

        if (jumpCooldownTimer.IncrementIfEnabled())
        {
            jumpCooldownTimer.Enabled = false;
            jumpCooldownTimer.Reset();
            jumpIsCoolingDown = false;
        }


    }

}
