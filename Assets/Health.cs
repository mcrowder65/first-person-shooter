using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{



    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = Constants.MAX_HEALTH;

	public RectTransform healthBar;

	public void TakeDamage (int amount)
	{
		if (!isServer)
			return;

		currentHealth -= amount;
		if (currentHealth <= 0) {
			currentHealth = Constants.MAX_HEALTH; 

			// called on the Server, but invoked on the Clients
			RpcRespawn ();
		}
	}

	void OnChangeHealth (int currentHealth)
	{
		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn ()
	{
		if (isLocalPlayer) {
			// move back to zero location
			transform.position = Utilities.getNewRespawnPoint ();
		}
	}
}