using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorController_HV : MonoBehaviour {
    public Animator animator;
    public string ParamNameH, ParamNameV;
    public float HSpeed, VSpeed;
    public KeyCode ModifierKey;
    float modVal = 1f;

    void FixedUpdate () {
		var x = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");

        if (Input.GetKey(ModifierKey))
            modVal = 2f;

        animator.SetFloat(ParamNameV, z * modVal * VSpeed);
        animator.SetFloat(ParamNameH, x * modVal * HSpeed);
    }
}
