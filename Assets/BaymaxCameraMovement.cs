using UnityEngine;
using System.Collections;

public class BaymaxCameraMovement : MonoBehaviour {

	

    public float speedV = 2.0f;

    private float pitch = -1f;
    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
        if(pitch == -1f)
        {
            pitch = transform.eulerAngles.x;
        }
        pitch -= speedV * Input.GetAxis(Utilities.isXboxController() ? "Right joystick vertical" : "Mouse Y");

        transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0.0f);
        // Clamp pitch:
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0f);
        
    }
}
