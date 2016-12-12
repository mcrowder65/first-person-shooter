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
                GetComponentInParent<PlayerController>().CmdDeath();
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
            GetComponentInParent<PlayerController>().CmdDeath();
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
        if(deaths > 100)
        {
            deaths = 0; // had to hard code this in... idk how it's getting set to 485 randomly...
        }
        scoreboard.GetComponent<UnityEngine.UI.Text>().text = "Kills: " + kills + " Deaths: " + deaths;
    }
	void OnChangeHealth (int currentHealth)
	{
        //TODO: Do we want to set currentHealth here too?
		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	public void RpcRespawn ()
	{
		if (isLocalPlayer) {
            Respawn newRespawn = Utilities.getNewRespawnPoint();
            var cam = GetComponent<PlayerController>().myCamera;
            Debug.Assert(cam != null);
            var gun = GetComponent<PlayerController>().currentWeapon;
            Debug.Assert(gun != null);

            newRespawn.setRespawn(gameObject, cam.gameObject, gun.gameObject);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentHealth = Constants.MAX_HEALTH;

        }
    }

    [Command]
    public void CmdRespawn()
    {
        RpcRespawn();
       
    }
}