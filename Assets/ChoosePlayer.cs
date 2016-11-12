using UnityEngine;
using System.Collections;

public class ChoosePlayer : MonoBehaviour {


    public GameObject CurrentlyChosenCharacter;
    public GameObject SelectedPlayerPanel;
    public GameObject ModelPlaceholder;
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

    public void ChangeCharacter(int character)
    {
        if (CurrentlyChosenCharacter != null)
            Destroy(CurrentlyChosenCharacter);
        CharacterEnum cEnum = (CharacterEnum)character;
        GameObject model = CharacterTools.GetCharacterModel(cEnum, this, null);
        model.transform.position = ModelPlaceholder.transform.position;
        CurrentlyChosenCharacter = model;
    }
}
