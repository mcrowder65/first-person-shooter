using UnityEngine;
using System.Collections;

public static class CharacterTools {


    public static GameObject GetCharacterModel(CharacterEnum character, MonoBehaviour caller, GameObject newParent)
    {
        string resourceName = null;
        switch (character) {
            case CharacterEnum.Baymax:
                resourceName =  "baymax";
                break;
            case CharacterEnum.Angel:
                resourceName = "angel";
                break;
            default:
                throw new UnityException("Unexpected character");

        }

        Object o = Object.Instantiate(Resources.Load(resourceName), newParent == null ? null : newParent.transform);
        if (o == null)
            throw new UnityException("instantiated object was null");
        GameObject go = o as GameObject;
        if (go == null)
            throw new UnityException("instantiated object was not of type GameObject.");
        return go;
    }
}
