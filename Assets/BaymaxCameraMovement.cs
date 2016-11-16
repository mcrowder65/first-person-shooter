using UnityEngine;
using System.Collections;

public class BaymaxCameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public float speedV = 2.0f;

    private float pitch = -1f;
    // Update is called once per frame

    void Update()
    {
        if(pitch == -1f)
        {
            pitch = transform.eulerAngles.x;
        }
        //TODO remove this if check when making the game live.
        if (Input.GetMouseButton(0))
        {
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0.0f);
            // Clamp pitch:
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0f);
       }
        
    }
}
