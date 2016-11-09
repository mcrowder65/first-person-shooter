using UnityEngine;
using System.Collections;

public class ChoosePlayer : MonoBehaviour {


    public GameObject CurrentlyChosenCharacter;
    const float ROTATE_SPEED = 10000f;



	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (CurrentlyChosenCharacter != null)
        {
            CurrentlyChosenCharacter.transform.Rotate(Vector3.up, 5);
        }
	}
}
