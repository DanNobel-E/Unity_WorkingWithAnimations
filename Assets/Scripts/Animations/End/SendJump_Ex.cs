using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendJump_Ex : MonoBehaviour
{
    public Animator Anim;
    public KeyCode JumpTriggerKey;
    public string JumpFromThisState, JumpState, JumpTrigger;
    AnimatorStateInfo animInfo;
    AnimatorTransitionInfo animTransitionInfo;

    //Take the name of the state and convert it into an integer so we can use it as a reference
    int jumpFromThisStateHash, jumpStateHash, toJumpTransitionHash;
    string toJumpTransitionName;

    void Start()
    {
        //Take the name of the state and convert it into an integer so we can use it as a reference
        jumpFromThisStateHash = Animator.StringToHash(JumpFromThisState);
        jumpStateHash = Animator.StringToHash(JumpState);
        toJumpTransitionName = JumpFromThisState + " -> " + JumpState;
        toJumpTransitionHash = Animator.StringToHash(toJumpTransitionName);
    }

    void Update()
    {
        animInfo = Anim.GetCurrentAnimatorStateInfo(0);
        animTransitionInfo = Anim.GetAnimatorTransitionInfo(0);
        bool isInTransition = Anim.IsInTransition(0);

        int currAnimStateHash = animInfo.shortNameHash;
        if (currAnimStateHash == jumpFromThisStateHash && Input.GetKeyDown(JumpTriggerKey) && !isInTransition)
            Anim.SetTrigger(JumpTrigger);
    }
}
