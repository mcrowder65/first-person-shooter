using UnityEngine;
using System.Collections;

public class BaymaxMovement : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	}
	public float moveSpeed = 30f;
	public float turnSpeed = 50f;
	void moveForward ()
	{
		Vector3 vect = new Vector3 (-1, 0, 0);
		transform.Translate (vect * moveSpeed * Time.deltaTime);
	}

	void moveBackwards ()
	{
		Vector3 vect = new Vector3 (1, 0, 0);
		transform.Translate (vect * moveSpeed * Time.deltaTime);
	}
    void moveLeft()
    {
        Vector3 vect = new Vector3(0, 0, -1);
        transform.Translate(vect * moveSpeed * Time.deltaTime);
    }

    void moveRight()
    {
        Vector3 vect = new Vector3(0, 0, 1);
        transform.Translate(vect * moveSpeed * Time.deltaTime);
    }
    void rotateHorizontally()
    {
        yaw += speedH * Input.GetAxis("Mouse X");

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, yaw, 0.0f);

        // Wrap yaw:
        while (yaw < 0f)
        {
            yaw += 360f;
        }
        while (yaw >= 360f)
        {
            yaw -= 360f;
        }
        // Set orientation:
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, yaw, 0f);
    }
    public float speedH = 2.0f;

    private float yaw = -1f;
    
    // Update is called once per frame
    void Update ()
	{
        if(yaw == -1f)
        {
            yaw = transform.eulerAngles.y;
        }
        //TODO remove this check when making the game live
        if (Input.GetMouseButton(0))
        {
            rotateHorizontally();
        }
        if (Input.GetKey (KeyCode.UpArrow)) {
			moveForward ();
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			moveBackwards ();
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveLeft ();
		}
            

		if (Input.GetKey (KeyCode.RightArrow)) {
			moveRight ();
		}
		if (Input.GetKeyUp (KeyCode.J))
			transform.Translate (new Vector3 (0, 1, 0));
	}
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}
