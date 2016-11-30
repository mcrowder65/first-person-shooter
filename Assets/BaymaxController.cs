
using UnityEngine;
using UnityEngine.Networking;
public class BaymaxController : PlayerController
{
    // Override anything necessary in here.
    [Command]
    public override void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        // Add velocity to the bullet
        var canvas = transform.Find("Canvas");
        var crossHair = canvas.Find("Crosshair");
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 100;
        bullet.GetComponent<Rigidbody>().position = crossHair.position;
        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 1.5f);
    }
}
