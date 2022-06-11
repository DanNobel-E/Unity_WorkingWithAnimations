using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController_Walk_Run_Jump : MonoBehaviour {
    public Animator animator;
    public string paramNameTranslate, paramNameRotate;
    public bool sendTranslateParam, sendRotateParam;
    public bool translateRoot, rotateRoot;
	public float rotationBoost = 1;
    public KeyCode RunKey, JumpKey;
    public string JumpTriggerName;

    public string[] JumpIIfInStates = new string[2] { "HumanoidRun", "HumanoidWalk" }; //Eg. "Base Layer.Walking"

    int[] statesToCheckHash;
    int currStateHash;
    AnimatorStateInfo animStateInfo;

    float modVal = 1f;

    private void Start()
    {
        for (int i = 0; i < JumpIIfInStates.Length; i++)
        {
            statesToCheckHash[i] = Animator.StringToHash(JumpIIfInStates[i]);

        }
    }

    void Update () {
		var x = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical")*50*Time.deltaTime;

		if (translateRoot)
            transform.Translate(0, 0, z);
		if(rotateRoot)
            transform.Rotate(0, x*rotationBoost, 0);

        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        currStateHash = animStateInfo.shortNameHash;

        if ((currStateHash == statesToCheckHash[0] || currStateHash == statesToCheckHash[1]) && Input.GetKeyDown(JumpKey))
        {
            animator.SetTrigger(JumpTriggerName);
        }

        if (Input.GetKey(RunKey))
            modVal = 2f;
        else
            modVal = 1f;

        if (sendTranslateParam && z != 0)
            animator.SetFloat(paramNameTranslate, z * modVal);
		if (sendRotateParam && x != 0)
            animator.SetFloat(paramNameRotate, x * 2.0f);

        //Debug.Log("Horizontal " + x);
    }
}
