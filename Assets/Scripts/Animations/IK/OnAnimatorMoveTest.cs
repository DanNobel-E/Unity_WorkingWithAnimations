using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimatorMoveMode
{
    DO_NOTHING,
    APP_BI_RMOTION,
    APP_RMOTION_MANUALLY,
    APP_RMOTION_MANUALLY_W_CHANGES,
}
public class OnAnimatorMoveTest : MonoBehaviour
{
    public AnimatorMoveMode AnimMoveMode;
    public float Amplitude = 0.01f, Frequency = 20;
    Vector3 currPos, newPos;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorMove()
    {
        Debug.Log("OnAnimatorMove");
        if (AnimMoveMode == AnimatorMoveMode.DO_NOTHING)
        {
        }
        else if (AnimMoveMode == AnimatorMoveMode.APP_BI_RMOTION)
        {
            anim.ApplyBuiltinRootMotion();
        }
        else if (AnimMoveMode == AnimatorMoveMode.APP_RMOTION_MANUALLY)
        {
            newPos = anim.rootPosition;
            transform.position = newPos;
        }
        else if (AnimMoveMode == AnimatorMoveMode.APP_RMOTION_MANUALLY_W_CHANGES)
        {
            currPos = anim.rootPosition;
            newPos = currPos + new Vector3(Mathf.Sin(Time.time * Frequency) * Amplitude, 0, 0);
            transform.position = newPos;
        }
    }
}
