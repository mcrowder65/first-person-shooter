using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        var x = Input.GetAxis("Right joystick horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Right joystick vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}