using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{



	[SyncVar (hook = "OnChangeHealth")]
	public int currentHealth = Constants.MAX_HEALTH;
	public int deaths = 0;
	public int kills = 0;
	public RectTransform healthBar;

	public void TakeDamage (int amount, Transform firer)
	{
		if (!isServer)
			return;

		currentHealth -= amount;
		if (currentHealth <= 0) {
			currentHealth = Constants.MAX_HEALTH;
			deaths++;
			Debug.Log (firer.GetComponent < Health> ().kills);
			
			firer.GetComponent<Health> ().kills++;
			firer.GetComponent<PlayerController> ().updateScoreboard ();
			GetComponentInParent<PlayerController> ().updateScoreboard ();
			// called on the Server, but invoked on the Clients
			RpcRespawn ();
		}
	}

	public void deathByFalling ()
	{
		currentHealth = Constants.MAX_HEALTH;
		transform.position = Utilities.getNewRespawnPoint ();
		deaths++;
		kills--;
	}

	void OnChangeHealth (int currentHealth)
	{
		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn ()
	{
		if (isLocalPlayer) {
			transform.position = Utilities.getNewRespawnPoint ();
		}
	}
}