using UnityEngine;
using System.Collections;
using System;

public class Killcam : MonoBehaviour {


    public event Action EndKillcam;
    const float FLOAT_AWAY_SPEED = 0.9f;
    const float QUICK_FLOAT_AWAY_SPEED = 5f;
    const float QUICK_RANGE_TIME = 1.2f;
    const float STARTING_Y_OFFSET = 30f;
    GenericTimer killcamTimer = new GenericTimer(4, false);

    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private Transform originalParent;
    
    public void BeginKillcam(GameObject target)
    {
        Camera camera = GetComponent<Camera>();
   
        originalRotation = transform.rotation;
        originalParent = target.transform;
        originalPosition = transform.localPosition;

        //Debug.Log("ORIG: " + originalPosition.ToString());
        transform.SetParent(null, true);
        transform.position = target.transform.position + Vector3.up * STARTING_Y_OFFSET;
        transform.LookAt(target.transform);
     
        StartCoroutine(FloatAwayAnimation());
    }

    IEnumerator FloatAwayAnimation()
    {
        killcamTimer.Enabled = true;
        float currentTime = 0f;
        while (killcamTimer.Enabled)
        {
            if (killcamTimer.IncrementIfEnabled())
            {
                killcamTimer.Enabled = false;
                killcamTimer.Reset();
                if (EndKillcam != null)
                    EndKillcam();
                transform.SetParent(originalParent, true);
                transform.rotation = originalRotation;
                transform.localPosition = originalPosition;

                //Debug.Log("NEW: " + transform.position.ToString());
                //GetComponent<Animator>().SetTrigger("EndKillcam");
                yield break;
            }
            else
            {
                
                currentTime += Time.deltaTime;
                float distance;
                if (currentTime > QUICK_RANGE_TIME)
                    distance = Time.deltaTime * FLOAT_AWAY_SPEED;
                else
                    distance = Time.deltaTime * (FLOAT_AWAY_SPEED + (QUICK_RANGE_TIME - currentTime) * QUICK_FLOAT_AWAY_SPEED);

                transform.position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
                yield return null;
            }
        }

    }
}
