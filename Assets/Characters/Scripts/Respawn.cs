using UnityEngine;
using System.Collections;

public class Respawn {
    private Vector3 position;
    private Vector3 playerRotation;
    private Vector3 cameraRotation;
    private Vector3 gunRotation;

    public Respawn(Vector3 position, Vector3 playerRotation, Vector3 cameraRotation, Vector3 gunRotation)
    {
        this.position = position;
        this.playerRotation = playerRotation;
        this.cameraRotation = cameraRotation;
        this.gunRotation = gunRotation;
    }
    public void setRespawn(Transform transform)
    {
        transform.position = position;
        var gun = transform.Find("SubmachineGun"); //TODO this needs to be anything rather than just submachine gun.
        var camera = transform.Find("Camera");
        //TODO set rotation here.
        //transform.eulerAngles = playerRotation;
        //camera.transform.eulerAngles = cameraRotation;
        //gun.transform.eulerAngles = gunRotation;
    }
}
