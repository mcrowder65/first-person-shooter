using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
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
			health.TakeDamage (10);
		}
		Destroy (gameObject);
	}
}
