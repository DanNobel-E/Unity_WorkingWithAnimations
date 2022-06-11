using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorInfo : MonoBehaviour {
	public Animator anim;
    public string FullPathToCheck = "Base Layer.HumanoidWalk"; 
    public string ShortNameToCheck = "HumanoidWalk";
    public string TransitionFromStateToCheck = "HumanoidIdle";
    public string TransitionToStateToCheck = "HumanoidWalk";
    public bool displayInfo = true;
    public Vector2 offset;


    int fullPathToCheckHash, shortNameToCheckHash;
    int currFullPathHash, currShortNameHash;
    AnimatorStateInfo animStateInfo;
    AnimatorTransitionInfo animTransitionInfo;
    bool isInTransition;
    string transitionToCheckName;
    string log;

	void Start () {
        //Take the name of the state and convert it into an integer so we can use it as a reference
    }

    void Update () {
        log = "";

        fullPathToCheckHash = Animator.StringToHash(FullPathToCheck);
        shortNameToCheckHash = Animator.StringToHash(ShortNameToCheck);
        isInTransition = anim.IsInTransition(0);

        //Get AnimatorState AND Transition Info
        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        animTransitionInfo = anim.GetAnimatorTransitionInfo(0);

        //Eg. "Base Layer.HumanoidWalk"
        currFullPathHash = animStateInfo.fullPathHash;
        //Eg. "HumanoidWalk"
        currShortNameHash = animStateInfo.shortNameHash;
        currFullPathHash = animStateInfo.shortNameHash;

        transitionToCheckName = TransitionFromStateToCheck + " -> " + TransitionToStateToCheck;

        PrintInfo();
    }

    void PrintInfo()
    {
        log += string.Format("AnimatorStateInfo:\nlength:{0}\nloop:{1}\nnormalizedTime:{2}\nspeed:{3}\nspeedMultiplier:{4}",
            animStateInfo.length, animStateInfo.loop, animStateInfo.normalizedTime, animStateInfo.speed, animStateInfo.speedMultiplier);

        if (fullPathToCheckHash == currFullPathHash)
            log += string.Format("\n-- Animator is in state {0}", FullPathToCheck);
        if (shortNameToCheckHash == currShortNameHash)
            log += string.Format("\n-- Animator is in state {0}", ShortNameToCheck);

        log += string.Format("\n-- isInTransition is {0}", isInTransition);
        if (animTransitionInfo.IsName(transitionToCheckName))
            log += string.Format("\n-- Animator transition: {0}", transitionToCheckName);
    }

    void OnGUI()
    {
        if (!displayInfo) return;
        GUILayout.BeginHorizontal();
        GUILayout.Space(offset.x);
        GUILayout.BeginVertical();
        GUILayout.Space(offset.y);
        GUILayout.BeginVertical("box");
        GUILayout.Label(log);
        GUILayout.EndVertical();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
}
