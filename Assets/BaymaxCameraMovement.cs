using UnityEngine;
using System.Collections;

public class BaymaxCameraMovement : MonoBehaviour {

	

    public float speedV = 2.0f;

    private float pitch = -1f;
    // Use this for initialization
    void Start()
    {

    }
    bool isXboxController()
    {
        return Input.GetJoystickNames()[0].ToString() == "Controller (XBOX 360 For Windows)";
    }
    void Update()
    {
        if(pitch == -1f)
        {
            pitch = transform.eulerAngles.x;
        }
        pitch -= speedV * Input.GetAxis(isXboxController() ? "Right joystick vertical" : "Mouse Y");

        transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0.0f);
        // Clamp pitch:
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0f);
        
    }
}
