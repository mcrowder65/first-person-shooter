using UnityEngine;
using System.Collections;

public class BaymaxGun : MonoBehaviour {
    bool isXboxController()
    {
        return Input.GetJoystickNames()[0].ToString() == "Controller (XBOX 360 For Windows)";
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
