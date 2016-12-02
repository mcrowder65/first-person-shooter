using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {


    public PlayerController owner;
    protected GenericTimer killTimer;

    public Projectile()
    {
        killTimer = new GenericTimer(Lifespan, true);
    }


	// Update is called once per frame
	void Update () {

        if (killTimer.IncrementIfEnabled())
        {
            Destroy(this.gameObject);
        }
	}

    public virtual float Lifespan
    {
        get { throw new UnityException("Lifespan was not overridden."); }
    }

}
