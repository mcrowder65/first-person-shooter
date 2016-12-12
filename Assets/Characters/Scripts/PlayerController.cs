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

	public Camera myCamera;


	//TODO: THIS IS TEMPORARY
	public GameObject SubmachinegunPrefab;
	public GameObject FlamethrowerPrefab;

	public Weapon currentWeapon;


	private GenericTimer jumpCooldownTimer = new GenericTimer (0.5f, false);
	private bool jumpIsCoolingDown = false;

	[SyncVar (hook = "OnDeadChanged")]
	private bool _dead = false;

	public bool Dead {
		get { return _dead; }
		set {
			_dead = value;
		}
	}

	[SyncVar (hook = "OnWalkingChanged")]
	private bool _walking = false;

	public bool Walking {
		get { return _walking; }
		protected set {
			bool old = _walking;
			// if (old != value)
			//{
			_walking = value;
			if (_walking)
				GetComponent<GameAnimator> ().BeginWalk (LeftLeg, RightLeg);
			else
				GetComponent<GameAnimator> ().EndWalk ();
			// }
		}
	}

	public void OnDeadChanged (bool dead)
	{
		Dead = dead;
	}

	public void OnWalkingChanged (bool walking)
	{
		Walking = walking;
	}

	//This could be overridden if necessary
	public GameObject LeftLeg {
		get {
			return GetPiece (PieceType.LeftLeg);
		}
	}

	public GameObject RightLeg {
		get {
			return GetPiece (PieceType.RightLeg);
		}
	}

	public GameObject GetPiece (PieceType type)
	{
		foreach (var p in GetComponentsInChildren<PlayerPiece>()) {
			if (p.TypeOfPiece == type)
				return p.gameObject;
		}
		return null;
	}



	// Use this for initialization
	public void Start ()
	{
		updateScoreboard ();
		Physics.gravity = new Vector3 (0, -50, 0);

		GetComponentInChildren<AudioListener> ().enabled = isLocalPlayer;

		GetComponentInChildren<Killcam> ().EndKillcam += PlayerController_EndKillcam;
		//TODO: THIS IS TEMPORARY



		GameObject weapon = (GameObject)Instantiate (Utilities.rand.Next (0, 2) == 0 ? SubmachinegunPrefab : FlamethrowerPrefab, this.transform, false);
		currentWeapon = weapon.GetComponent<Weapon> ();

		myCamera = GetComponentInChildren<Camera> ();


		if (isLocalPlayer) {
			PlayerController[] allPlayers = GameObject.FindObjectsOfType<PlayerController> ();
			Respawn newRespawn = null;
			foreach (var p in allPlayers) {
				if (p.gameObject != gameObject)
					newRespawn = Utilities.getNewRespawnPoint (p.gameObject);
			}
			if (newRespawn == null)
				newRespawn = Utilities.getRandomRespawnPoint ();

			var cam = myCamera;
			Debug.Assert (cam != null);
			var gun = currentWeapon;
			Debug.Assert (gun != null);

			newRespawn.setRespawn (gameObject, cam.gameObject, gun.gameObject);



		}
		gameObject.name = isLocalPlayer ? "Baymax (Me) " : "Baymax (Enemy)";
		/*
            if (Network.isServer)
                gameObject.name = isLocalPlayer ? "Baymax (Server)" : "Baymax (Client)";
            else
                gameObject.name = isLocalPlayer ? "Baymax (Client)" : "Baymax (Server)";
        */
	}

	private void PlayerController_EndKillcam ()
	{
		Dead = false;
		Destroy (GetComponentInChildren<Weapon> ().gameObject);
		GameObject weapon = (GameObject)Instantiate (Utilities.rand.Next (0, 2) == 0 ? SubmachinegunPrefab : FlamethrowerPrefab, this.transform, false);
		currentWeapon = weapon.GetComponent<Weapon> ();
		GetComponent<Health> ().CmdRespawn ();
	}

	public override void OnStartLocalPlayer ()
	{
        
    
	}

	void moveForwardsOrBackwards (float val)
	{
		if (Dead)
			return;

		Vector3 vect = new Vector3 (val, 0, 0);
		transform.Translate (vect * moveSpeed * Time.deltaTime);
	}

	void moveLeftOrRight (float val)
	{
		if (Dead)
			return;

		Vector3 vect = new Vector3 (0, 0, val);
		transform.Translate (vect * moveSpeed * Time.deltaTime);
	}

    
	void CmdRotateVertically ()
	{
		if (Dead)
			return;

		Camera cam = myCamera;
		//TODO: idk if we'll be able to generalize this...
		var gun = transform.Find ("SubmachineGun");//currentWeapon;
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

  
	void CmdRotateHorizontally ()
	{
		if (Dead)
			return;

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
		if (Dead)
			return;

		if (!jumpIsCoolingDown && IsOnGround ()) {
			GetComponent<Rigidbody> ().AddForce (Vector3.up * Constants.JUMP_FORCE);
			jumpCooldownTimer.Enabled = true;
			jumpIsCoolingDown = true;
		}
	}

	bool IsOnGround ()
	{
		Collider lowestCollider = null;
		float lowY = float.MaxValue;
		foreach (var collider in GetComponentsInChildren<Collider>()) {
			if (collider.transform.position.y < lowY) {
				lowY = collider.transform.position.y;
				lowestCollider = collider;
			}
		}

		float raycastDistance = lowestCollider.bounds.extents.y + 0.8f;
		return Physics.Raycast (transform.position, -Vector3.up, raycastDistance);
	}

	bool deathByFalling ()
	{
		if (Dead)
			return false;
		if (transform.position.y > Utilities.GetDeathY (Utilities.GetCurrentScene ())) {
			return false;
		}
		var health = GetComponent<Health> ();
		health.CmdDeathByFalling ();
		return true;
	}

	[Command]
	public void CmdFire ()
	{
		if (Dead)
			return;

		if (!currentWeapon.CanFire)
			return;

		// Create the Bullet from the Bullet Prefab
		var canvas = transform.Find ("Canvas");
		var crossHair = canvas.Find ("Crosshair");
		Projectile projectile = currentWeapon.CmdCreateProjectile (crossHair);
		Destroy (projectile.gameObject, 2f);
		NetworkServer.Spawn (projectile.gameObject);
		projectile.RpcSetRotation ();

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
			if (GetComponentInChildren<Camera> () != null) {
				GetComponentInChildren<Camera> ().enabled = false;
			}
			
			return;
		}

		if (deathByFalling ()) {
			return;
		}
		var health = GetComponent<Health> ();
		//Debug.Log("kills: " + health.kills + " deaths: " + health.deaths);
		CmdRotateHorizontally ();
		CmdRotateVertically ();
		if (Utilities.isXboxController ()) {
			moveLeftOrRight (Input.GetAxis ("Left joystick left right"));
			moveForwardsOrBackwards (Input.GetAxis ("Left joystick forwards backwards"));
			if (Input.GetKeyUp (KeyCode.JoystickButton0)) {
				jump ();
			}
			float triggerInput = Input.GetAxis ("Triggers");
			if (triggerInput > 0) { //left trigger
				Debug.Log ("Grenade!!!");
			} else if (triggerInput < 0) { //right trigger
				CmdFire ();
			}
		} else {
			bool moved = false;
			if (Input.GetKey (KeyCode.A)) {
				moveLeftOrRight (-1);
				moved = true;
			}
			if (Input.GetKey (KeyCode.D)) {
				moveLeftOrRight (1);
				moved = true;
			}
			if (Input.GetKey (KeyCode.W)) {
				moveForwardsOrBackwards (-1);
				moved = true;
			}
			if (Input.GetKey (KeyCode.S)) {
				moveForwardsOrBackwards (1);
				moved = true;
			}
			Walking = moved && !Dead;
    

			if (Input.GetKeyUp (KeyCode.Space)) {
				jump ();
			}
			if (Input.GetKey (KeyCode.Mouse0)) {
               
				var canvas = transform.Find ("Canvas");
				var crossHair = canvas.Find ("Crosshair");
				Vector3 crossHairPosition = crossHair.position;
				CmdFire ();
			}
           
		}

		if (jumpCooldownTimer.IncrementIfEnabled ()) {
			jumpCooldownTimer.Enabled = false;
			jumpCooldownTimer.Reset ();
			jumpIsCoolingDown = false;
		}


	}

}
