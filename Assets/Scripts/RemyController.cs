using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyController : MonoBehaviour
{
    public RaycastBoxContainer_Start Script;
    public Animator animator;
    public string paramNameTranslate, paramNameRotate;
    public bool sendTranslateParam, sendRotateParam;
    public bool translateRoot, rotateRoot;
    public float rotationBoost = 1;
    public KeyCode RunKey, JumpKey;
    public string JumpTriggerName;
    public string MTStateName;
    public string MTPrevStateName;
    string MTTransitionName;


    public string[] JumpIIfInStates = new string[2] { "HumanoidRun", "HumanoidWalk" }; //Eg. "Base Layer.Walking"

    int[] statesToCheckHash= new int[2];
    int currStateHash;

    int MTStateHash;
    int MTPrevStateHash;
    AnimatorStateInfo animatorStateInfo;
    AnimatorTransitionInfo animatorTransitionInfo;

    public Transform NextMTargetTransform;
    public float StartMT, EndMT;
    public Vector3 MTPosW;
    public float MTRotW;


    bool OnJumpTrigger=false;
    float modVal = 1f;

    private void Start()
    {
        

        for (int i = 0; i < JumpIIfInStates.Length; i++)
        {
            statesToCheckHash[i] = Animator.StringToHash(JumpIIfInStates[i]);

        }
        MTStateHash = Animator.StringToHash(MTStateName);
        MTPrevStateHash = statesToCheckHash[1];
        MTTransitionName = MTPrevStateName + " -> " + MTStateName;

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical") * 100 * Time.deltaTime;

        if (Input.GetKey(RunKey))
            modVal = 2.5f;
        else
            modVal = 1f;

        if (sendTranslateParam && z != 0)
            animator.SetFloat(paramNameTranslate, z * modVal);
        if (sendRotateParam && x != 0)
            animator.SetFloat(paramNameRotate, x * 2.0f);

        if (translateRoot)
            transform.Translate(0, 0, z*modVal);
        if (rotateRoot)
            transform.Rotate(0, x * rotationBoost, 0);

        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        currStateHash = animatorStateInfo.shortNameHash;
        animatorTransitionInfo = animator.GetAnimatorTransitionInfo(0);



        if ((currStateHash == statesToCheckHash[0] || currStateHash == statesToCheckHash[1]) && Input.GetKeyDown(JumpKey) && !animator.IsInTransition(0) && OnJumpTrigger)
        {
            animator.SetTrigger(JumpTriggerName);
        }

        if (currStateHash == MTStateHash ||
           (currStateHash == MTPrevStateHash && animatorTransitionInfo.IsName(MTTransitionName)))
        {
            animator.SetFloat(paramNameTranslate, 0);
            animator.MatchTarget(NextMTargetTransform.position, NextMTargetTransform.rotation, AvatarTarget.LeftHand, new MatchTargetWeightMask(MTPosW, MTRotW), StartMT, EndMT);
            animator.MatchTarget(NextMTargetTransform.position, NextMTargetTransform.rotation, AvatarTarget.Root, new MatchTargetWeightMask(MTPosW, MTRotW), EndMT, 0.65f);

        }

    }

    public void SetJumpTrigger()
    {
        OnJumpTrigger = !OnJumpTrigger;
    }
}

