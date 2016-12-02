using UnityEngine;
using System.Collections;
using System;

public class ParticleColliderNotifier : MonoBehaviour {

    public event Action<GameObject> Collided;
    void OnParticleCollision(GameObject collision)
    {
        if (Collided != null)
            Collided(collision);
    }
}
