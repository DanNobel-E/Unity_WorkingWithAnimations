using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMatchTarget : MonoBehaviour
{
    public Animator Animator;
    public Transform NextMTargetTransform;

    public string MTStateName;
    public string MTPrevStateName;
    public float StartMT, EndMT;
    public Vector3 MTPosW;
    public float MTRotW;

    string MTTransitionName;
    int MTStateHash;
    int MTPrevStateHash;
    int currStateHash;
    AnimatorStateInfo animatorStateInfo;
    AnimatorTransitionInfo animatorTransitionInfo;


    // Start is called before the first frame update
    void Start()
    {
        MTStateHash = Animator.StringToHash(MTStateName);
        MTPrevStateHash = Animator.StringToHash(MTPrevStateName);
        MTTransitionName = MTPrevStateName + " -> " + MTStateName;
    }

    // Update is called once per frame
    void Update()
    {
        animatorStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        animatorTransitionInfo = Animator.GetAnimatorTransitionInfo(0);
        currStateHash = animatorStateInfo.shortNameHash;

        

        if(currStateHash==MTStateHash || 
            (currStateHash==MTPrevStateHash && animatorTransitionInfo.IsName(MTTransitionName)))
        {

        Animator.MatchTarget(NextMTargetTransform.position, NextMTargetTransform.rotation, AvatarTarget.RightHand, new MatchTargetWeightMask(MTPosW, MTRotW), StartMT, EndMT);
        }

    }
}
