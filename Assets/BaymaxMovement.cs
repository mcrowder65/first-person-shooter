using UnityEngine;
using System.Collections;

public class BaymaxMovement : MonoBehaviour
{

	
	public float moveSpeed = 30f;
	public float turnSpeed = 50f;
    public float speedH = 2.0f;
    private float yaw = -1f;
    private float previousXAxis = 0f;
    // Use this for initialization
    void Start()
    {
    }
    bool isXboxController()
    {
        return Input.GetJoystickNames()[0].ToString() == "Controller (XBOX 360 For Windows)";
    }
    void moveForwardsOrBackwards (float val)
	{
		Vector3 vect = new Vector3 (val, 0, 0);
		transform.Translate (vect * moveSpeed * Time.deltaTime);
	}

    void moveLeftOrRight(float val)
    {
        Vector3 vect = new Vector3(0, 0, val);
        transform.Translate( vect * moveSpeed * Time.deltaTime);
    }

    void rotateHorizontally()
    {
        if (yaw == -1f)
        {
            yaw = transform.eulerAngles.y;
        }
        
        yaw += speedH * Input.GetAxis(isXboxController() ? "Right joystick horizontal" : "Mouse X");
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
    
    
    // Update is called once per frame
    void Update ()
	{
        rotateHorizontally();
        
        if(isXboxController())
        {
            moveLeftOrRight (Input.GetAxis("Left joystick left right"));
            moveForwardsOrBackwards(Input.GetAxis("Left joystick forwards backwards"));
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveLeftOrRight(-1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveLeftOrRight(1);
            }
            if (Input.GetKey(KeyCode.W))
            {
                moveForwardsOrBackwards(-1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveForwardsOrBackwards(1);
            }
        }
		
		if (Input.GetKeyUp (KeyCode.J))
			transform.Translate (new Vector3 (0, 1, 0));

    }

}
