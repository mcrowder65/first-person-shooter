using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class SubmachineGun : Weapon {

    public GameObject bulletPrefab;

    
    public override Projectile CmdCreateProjectile(Transform crosshair)
    {
        var bullet = (GameObject)Instantiate(
                         bulletPrefab,
                         crosshair.position,
                         crosshair.rotation,
                        // Quaternion.Euler(transform.parent.rotation.eulerAngles.x + pitch, transform.parent.rotation.eulerAngles.y - 90, transform.parent.rotation.eulerAngles.z),
                         null);


        Vector3 preForward = bullet.transform.forward;
        bullet.transform.Rotate(Vector3.up, 90);
        bullet.GetComponent<Bullet>().dummyEulerRotation = bullet.transform.eulerAngles;


        bullet.GetComponent<Rigidbody>().velocity = preForward * 100;
        bullet.GetComponent<Bullet>().owner = transform.parent.gameObject;
        Debug.Assert(bullet.GetComponent<Bullet>().owner != null);

        //TODO figure out how to rotate bullet correctly on client and server.
     //    bullet.transform.Rotate(Vector3.up, 90);
        GetComponent<AudioSource>().Play();

        RaiseShotFired();

        return bullet.GetComponent<Bullet>();
    }

    // Use this for initialization
    void Start () {
	
	}


    public override float CooldownPeriod
    {
        get
        {
            return 0.1f;
        }
    }
}
