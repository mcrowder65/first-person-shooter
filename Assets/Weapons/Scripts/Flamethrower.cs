using UnityEngine;
using System.Collections;
using System;

public class Flamethrower : Weapon {


    public GameObject flamePrefab;
    
    public override Projectile CreateProjectile(Transform crosshair)
    {
      //  Vector3 pos;
       // Vector3 forwardY = Quaternion.Euler(0.0f, pitch, 0.0f) * Vector3.forward;
       // Vector3 forward = transform.forward;
       // Vector3 right = transform.right;
      //  Vector3 up = transform.up;
       // Quaternion rotation = Quaternion.identity;
        GameObject flame = (GameObject)GameObject.Instantiate(flamePrefab,
                                                            crosshair.position, 
                                                           Quaternion.Euler(transform.parent.rotation.eulerAngles.x + pitch, transform.parent.rotation.eulerAngles.y - 90, transform.parent.rotation.eulerAngles.z),
                                                           this.transform);

        // set the start point near the player
        //  rotation = transform.rotation;
        //pos = transform.position + forward + right + up;

        // flame.transform.position = pos;
        //flame.transform.rotation = rotation;

        flame.GetComponent<Flame>().owner = transform.parent.GetComponent<PlayerController>();

        RaiseShotFired();

        return flame.GetComponent<Flame>();
    }

    public override float CooldownPeriod
    {
        get
        {
            return 1f;
        }
    }

}
