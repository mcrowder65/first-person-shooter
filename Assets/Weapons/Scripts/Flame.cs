using UnityEngine;
using System.Collections;

public class Flame : Projectile {




	// Use this for initialization
	void Start () {
        ParticleColliderNotifier collisionNotifier = this.transform.GetComponentInChildren<ParticleColliderNotifier>();
        collisionNotifier.Collided += ParticleCollided;
	}

    private void ParticleCollided(GameObject hit)
    {
   
        if (!IsHittable(hit))
            return;



        var health = hit.GetComponent<Health>();
        if (health != null)
        {

            health.TakeDamage(3, owner.transform);
        }
      
    }

    public override float Lifespan
    {
        get
        {
            return 3f;
        }
    }
}
