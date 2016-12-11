using UnityEngine;
using System.Collections;

public class Bullet : Projectile
{

  
	// Use this for initialization
	void Start ()
	{
       
	}

	void OnCollisionEnter (Collision collision)
	{
		var hit = collision.gameObject;

		if (!IsHittable (hit) || owner.transform == hit.transform)
			return;
       


		var health = hit.GetComponent<Health> ();
		if (health != null) {
			Debug.Assert (owner != null);
			health.TakeDamage (5, owner.transform);
		}
		Destroy (gameObject);
	}

	public override float Lifespan {
		get {
			return 1.5f;
		}
	}
}
