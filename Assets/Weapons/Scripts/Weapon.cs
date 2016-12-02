using UnityEngine;
using System.Collections;
using System;

public abstract class Weapon : MonoBehaviour {


    public float pitch = -1f;

    public GenericTimer cooldownTimer;


    public event Action ShotFired;
    public Weapon()
    {
        cooldownTimer = new GenericTimer(CooldownPeriod, false);
        ShotFired += OnShotFired;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	     if ( cooldownTimer.IncrementIfEnabled())
        {
            cooldownTimer.Enabled = false;
            cooldownTimer.Reset();
            
        }
	}

    protected void RaiseShotFired()
    {
        if (ShotFired != null)
            ShotFired();
    }

    public bool CanFire
    {
        get
        {
            return !cooldownTimer.Enabled;
        }
    }

    private void OnShotFired()
    {
        cooldownTimer.Enabled = true;
    }

    public abstract Projectile CreateProjectile(Transform crosshair);
    public virtual float CooldownPeriod
    {
        get { throw new UnityException("CooldownPeriod was not overridden."); }
    }
}
