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
       
		if (hit.ToString () == "Bullet(Clone) (UnityEngine.GameObject)") {
			return;
		}
		// TODO if hit is the object shooting it, or hit is another bullet, do nothing.
		var health = hit.GetComponent<Health> ();
		if (health != null) {
			
			health.TakeDamage (10, owner.transform);
		}
		Destroy (gameObject);
	}
    public override float Lifespan
    {
        get
        {
            return 1.5f;
        }
    }
}
