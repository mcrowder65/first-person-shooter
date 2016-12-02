using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {


    public float pitch = -1f;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract Projectile CreateProjectile(Transform crosshair);
}
