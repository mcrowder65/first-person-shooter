using UnityEngine;
using System.Collections;

public class GameAnimator : MonoBehaviour {

    const float FALL_TIME = 1.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DoFallAnimation()
    {
        StartCoroutine(FallAnimation());
    }

    IEnumerator FallAnimation()
    {


        Vector3 oldRotation = transform.rotation.eulerAngles;
        Vector3 destRotation = new Vector3(oldRotation.x, oldRotation.y, 90);
        float currTime = 0f;
        while (currTime < FALL_TIME)
        {
            currTime += Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(oldRotation, destRotation, currTime / FALL_TIME);
            yield return null;
        }
        yield break;


    }
}
