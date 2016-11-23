using UnityEngine;
using System.Collections;

public class BaymaxGun : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	}

	void Fire ()
	{
//		// Create the Bullet from the Bullet Prefab
//		var bullet = (GameObject)Instantiate (
//			                   bulletPrefab,
//			                   bulletSpawn.position,
//			                   bulletSpawn.rotation);
//
//		// Add velocity to the bullet
//		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 6;
//
//		// Destroy the bullet after 2 seconds
//		Destroy (bullet, 2.0f);
	}
	// Update is called once per frame
	void Update ()
	{
        
		if (Utilities.isXboxController ()) {

			float triggerInput = Input.GetAxis ("Triggers");
			//Debug.Log(triggerInput);
			if (triggerInput > 0) { //left trigger
				Debug.Log ("Grenade!!!");
			} else if (triggerInput < 0) { //right trigger
//				Fire ();
			}
            
		} else {
			if (Input.GetKey (KeyCode.Mouse0)) {
				Debug.Log ("shoot!!!");
			}
		}
	}
}
