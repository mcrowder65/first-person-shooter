using UnityEngine;
using System.Collections;

public class Bullet : Projectile
{

    public AudioSource firingSound;

	// Use this for initialization
	void Start ()
	{
        if (firingSound != null)
            firingSound.Play();   
	}
	
	void OnCollisionEnter (Collision collision)
	{
		var hit = collision.gameObject;

        if (!IsHittable(hit))
            return;



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
