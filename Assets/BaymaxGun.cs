using UnityEngine;
using System.Collections;

public class BaymaxGun : MonoBehaviour {
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	    if(Utilities.isXboxController())
        {

            float triggerInput = Input.GetAxis("Triggers");
            //Debug.Log(triggerInput);
            if (triggerInput > 0)
            { //left trigger
                Debug.Log("Grenade!!!");
            }
            else if (triggerInput < 0)
            { //right trigger
                Debug.Log("shoot!!!");
            }
            
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("shoot!!!");
            }
        }
	}
}
