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
    const float MAX_ROT = 40f;
    const float MIN_ROT = -MAX_ROT;
    const float WALK_SPEED = 290f;
    bool stoppedWalking = false;
    bool walkingRightNow = false;
    public void BeginWalk(GameObject leftLeg, GameObject rightLeg)
    {
        if (walkingRightNow) return;

        StartCoroutine(WalkAnimation(leftLeg, rightLeg, 10f));
    }
    public void EndWalk()
    {
        if (walkingRightNow)
        stoppedWalking = true;
    }
    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

    IEnumerator WalkAnimation(GameObject leftLeg, GameObject rightLeg, float waistOffset)
    {
        walkingRightNow = true;
        Vector3 rlOrigRotation = rightLeg.transform.eulerAngles;
        Vector3 llOrigRotation = leftLeg.transform.eulerAngles;
        //Right leg forward first
        float currRot = 0f;
        bool rightForward = true;

        Vector3 lPivot = leftLeg.transform.position +  Vector3.up * waistOffset;
        Vector3 rPivot =  rightLeg.transform.position + Vector3.up * waistOffset;
        Debug.Log("Pivot: " + lPivot + " ,  " + rPivot);
        //  Vector3 forwardAxis = leftLeg.transform.TransformDirection(Vector3.up);
        // Vector3 backAxis = leftLeg.transform.TransformDirection(Vector3.down);
        Vector3 ax1 = Vector3.forward;
        ax1.Normalize();
        Vector3 ax2 = ax1 * -1;
        while (!stoppedWalking)
        {
            if (!stoppedWalking)
            {
                currRot += rightForward ? (Time.deltaTime * WALK_SPEED) : -(Time.deltaTime * WALK_SPEED);

                float diff;
                if (rightForward && currRot > MAX_ROT)
                { rightForward = false; currRot = MAX_ROT; }
                else if (!rightForward && currRot < MIN_ROT)
                { rightForward = true; currRot = MIN_ROT; }

                diff = (MAX_ROT - Mathf.Abs(currRot)) / MAX_ROT;
                float speed = Time.deltaTime * WALK_SPEED * diff;

                //rightLeg.transform.Rotate(rightForward ? ax1 : ax2, Time.deltaTime * WALK_SPEED);
                //leftLeg.transform.Rotate(rightForward ? ax2 : ax1, Time.deltaTime * WALK_SPEED);
                rightLeg.transform.Rotate(rightForward ? ax1 : ax2, speed);
                leftLeg.transform.Rotate(rightForward ? ax2 : ax1, speed);
                //   rightLeg.transform.RotateAround(rPivot, rightForward ? forwardAxis : backAxis, Time.deltaTime * WALK_SPEED);
                // leftLeg.transform.RotateAround(lPivot,rightForward ? backAxis : forwardAxis, Time.deltaTime * WALK_SPEED);



                yield return null;
            }
        }
      
        rightLeg.transform.localRotation = Quaternion.identity;
        leftLeg.transform.localRotation = Quaternion.identity;
        stoppedWalking = false;
        walkingRightNow = false;
    }
}
