using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{



	[SyncVar (hook = "OnChangeHealth")]
	public int currentHealth = Constants.MAX_HEALTH;
    [SyncVar (hook = "OnChangeDeaths")]
	public int deaths = 0;
    [SyncVar (hook = "OnChangeKills")]
	public int kills = 0;
	public RectTransform healthBar;

	public void TakeDamage (int amount, Transform firer)
	{
		//if (!isServer)
		//	return;

        if (!GetComponentInParent<PlayerController>().Dead)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                
                currentHealth = Constants.MAX_HEALTH;
                ++deaths;

                firer.GetComponent<Health>().kills++;
                GetComponentInParent<PlayerController>().Death();
            }
        }
	}
        
   	public void deathByFalling ()
	{
        if (!GetComponentInParent<PlayerController>().Dead)
        {
            currentHealth = 0;
            ++deaths;
            --kills;
            GetComponentInParent<PlayerController>().Death();
        }
    }
    void OnChangeDeaths(int deaths) {
        this.deaths = deaths;
        updateScoreboard();
    }

    void OnChangeKills( int kills)
    {
        this.kills = kills;
        updateScoreboard();
    }
    void updateScoreboard()
    {
        var baymax = GetComponentInParent<PlayerController>();
        var canvas = baymax.transform.Find("Canvas");
        var scoreboard = canvas.transform.Find("Scoreboard");
        scoreboard.GetComponent<UnityEngine.UI.Text>().text = "Kills: " + kills + " Deaths: " + deaths;
    }
	void OnChangeHealth (int currentHealth)
	{
		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	public void RpcRespawn ()
	{
		if (isLocalPlayer) {
            Respawn newRespawn = Utilities.getNewRespawnPoint();
            newRespawn.setRespawn(transform);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentHealth = Constants.MAX_HEALTH;
		}
	}
}