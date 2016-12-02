using UnityEngine;
using System.Collections;
using System;

public class SubmachineGun : Weapon {

    public GameObject bulletPrefab;


    public override Projectile CreateProjectile(Transform crosshair)
    {
        var bullet = (GameObject)Instantiate(
                         bulletPrefab,
                         crosshair.position,
                         Quaternion.Euler(transform.parent.rotation.eulerAngles.x + pitch, transform.parent.rotation.eulerAngles.y - 90, transform.parent.rotation.eulerAngles.z),
                         transform);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 100;
        bullet.GetComponent<Bullet>().owner = transform.parent.GetComponent<PlayerController>();
        Debug.Assert(bullet.GetComponent<Bullet>().owner != null);
        return bullet.GetComponent<Bullet>();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
