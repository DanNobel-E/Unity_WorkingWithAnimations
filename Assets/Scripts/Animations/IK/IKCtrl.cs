using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKCtrl : MonoBehaviour {
	public Animator anim;
	public float posWeight, rotWeight;
	public Transform targetObjT,Hint;
	public AvatarIKGoal IKGoal;
    private float hintWeight;

    //Callback for setting up animation IK (inverse kinematics).
    //is called by the Animator Component immediately before it updates its internal IK system.
    //This callback can be used to set the positions of the IK goals and their respective weights.
    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight (IKGoal, posWeight);
		anim.SetIKPosition (IKGoal, targetObjT.position);
		anim.SetIKRotationWeight (IKGoal, rotWeight);
		anim.SetIKRotation(IKGoal, targetObjT.rotation);

		anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, hintWeight);
		anim.SetIKHintPosition(AvatarIKHint.RightElbow, Hint.position);

	}
}
