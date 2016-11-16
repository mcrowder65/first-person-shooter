using UnityEngine;
using System.Collections;

public class BaymaxGun : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    // Use this for initialization
    void Start () {
        bulletSpawn.position = new Vector3(-35.61617f, 10.6518f, 0.3926794f);
        bulletSpawn.rotation = new Quaternion(0, 0, 0, 0);
	}
    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
    // Update is called once per frame
    void Update ()
    {
        
	    if(Utilities.isXboxController())
        {

            float triggerInput = Input.GetAxis("Triggers");
            //Debug.Log(triggerInput);
            if (triggerInput > 0)
            { //left trigger
                Debug.Log("Grenade!!!");
            }
            else if (triggerInput < 0)
            { //right trigger
                Fire();
            }
            
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("shoot!!!");
            }
        }
	}
}
