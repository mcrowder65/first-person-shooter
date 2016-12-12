using UnityEngine;
using System.Collections;

public class Respawn {
    public Vector3 position;
    public Vector3 playerRotation;
    public Vector3 cameraRotation;
    public Vector3 gunRotation;

    public Respawn(Vector3 position, Vector3 playerRotation, Vector3 cameraRotation, Vector3 gunRotation)
    {
        this.position = position;
        this.playerRotation = playerRotation;
        this.cameraRotation = cameraRotation;
        this.gunRotation = gunRotation;
    }
    public void setRespawn(GameObject player, GameObject camera, GameObject gun)
    {
        player.transform.position = position;
        player.transform.eulerAngles = playerRotation;
        camera.transform.eulerAngles = cameraRotation;
        gun.transform.eulerAngles = gunRotation;
      //  var gun = transform.Find("SubmachineGun"); //TODO this needs to be anything rather than just submachine gun.
       // var camera = transform.Find("Camera");
        //TODO set rotation here.
        //transform.eulerAngles = playerRotation;
        //camera.transform.eulerAngles = cameraRotation;
        //gun.transform.eulerAngles = gunRotation;
    }
}
