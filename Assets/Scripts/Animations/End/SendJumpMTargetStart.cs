    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendJumpMTargetStart : MonoBehaviour
{
    public Animator Anim;
    public KeyCode JumpTriggerKey;
    public string JumpFromThisState, JumpState, JumpTrigger;
    AnimatorStateInfo animInfo;
    AnimatorTransitionInfo animTransitionInfo;

    int jumpFromThisStateHash, jumpStateHash;
    string toJumpTransitionName;

    public Transform MT_Target;
    public float StartMT, EndMT;
    public Vector3 MT_PosW;
    public float MT_RotW;

    void Start()
    {
        //Take the name of the state and convert it into an integer so we can use it as a reference
        jumpFromThisStateHash = Animator.StringToHash(JumpFromThisState);
        jumpStateHash = Animator.StringToHash(JumpState);
        toJumpTransitionName = JumpFromThisState + " -> " + JumpState;
    }

    void Update()
    {
        animInfo = Anim.GetCurrentAnimatorStateInfo(0);
        animTransitionInfo = Anim.GetAnimatorTransitionInfo(0);
        bool isInTransition = Anim.IsInTransition(0);

        int currAnimStateHash = animInfo.shortNameHash;
        if (currAnimStateHash == jumpFromThisStateHash && Input.GetKeyDown(JumpTriggerKey) && !isInTransition)
            Anim.SetTrigger(JumpTrigger);

        if(currAnimStateHash== jumpStateHash || (currAnimStateHash==jumpFromThisStateHash && animTransitionInfo.IsName(toJumpTransitionName)))
        {
            Anim.MatchTarget(MT_Target.position,MT_Target.rotation,AvatarTarget.Root, new MatchTargetWeightMask(MT_PosW, MT_RotW), StartMT, EndMT);
        }
    }
}
