using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplePlayerControllerAnimator : MonoBehaviour {
    public Animator animator;
    public string paramNameTranslate, paramNameRotate;
    public bool sendTranslateParam, sendRotateParam;
    public bool translateRoot, rotateRoot;
	public float rotationBoost = 1;
    public KeyCode ModifierKey;
    float x, z, ModVal;

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

    }
    void FixedUpdate () {

        if (rotateRoot)
            transform.Rotate(0, x * rotationBoost, 0);
        if (translateRoot)
            transform.Translate(0, 0, z * 0.1f);

        ModVal = 1f;
        if (Input.GetKey(ModifierKey))
            ModVal = 2.0f;

		if (sendTranslateParam && z != 0)
            animator.SetFloat(paramNameTranslate, z * ModVal);
		if (sendRotateParam && x != 0)
            animator.SetFloat(paramNameRotate, x * rotationBoost);
    }
}
