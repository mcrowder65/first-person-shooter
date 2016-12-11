using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class Projectile : NetworkBehaviour
{

    [SyncVar(hook = "OnChangeOwner")]
    public GameObject owner;
    protected GenericTimer killTimer;

    [SyncVar(hook = "OnChangeDummyEulerRotation")]
    public Vector3 dummyEulerRotation;

    public Projectile()
    {
        killTimer = new GenericTimer(Lifespan, true);
    }

    public void OnChangeOwner(GameObject owner)
    {
        this.owner = owner;
    }
    public void OnChangeDummyEulerRotation(Vector3 dummyEulerRotation)
    {
        this.dummyEulerRotation = dummyEulerRotation;
        this.transform.eulerAngles = dummyEulerRotation;
    }


    // Update is called once per frame
    void Update () {

        if (killTimer.IncrementIfEnabled())
        {
            Destroy(this.gameObject);
        }

	}

    [ClientRpc]
    public void RpcSetRotation()
    {
        this.transform.eulerAngles = dummyEulerRotation;
    }

    public virtual float Lifespan
    {
        get { throw new UnityException("Lifespan was not overridden."); }
    }

 
    protected bool IsHittable(GameObject hit)
    {
        return hit.GetComponent<Projectile>() == null;
    }

}
