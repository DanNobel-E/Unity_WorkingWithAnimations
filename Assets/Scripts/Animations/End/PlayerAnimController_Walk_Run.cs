using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController_Walk_Run : MonoBehaviour {
    public Animator animator;
    public string paramNameTranslate, paramNameRotate;
    public bool sendTranslateParam, sendRotateParam;
    public bool translateRoot, rotateRoot;
	public float rotationBoost = 1;
    public KeyCode ModifierKey;
    float modVal = 1f;

    void FixedUpdate () {
		var x = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");

		if (translateRoot)
            transform.Translate(0, 0, z*0.1f);
		if(rotateRoot)
            transform.Rotate(0, x*rotationBoost, 0);

        
        if (Input.GetKey(ModifierKey))
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
