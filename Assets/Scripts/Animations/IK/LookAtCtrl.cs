using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCtrl : MonoBehaviour {
	public Animator anim;
	public float lookWeight, bodyWeight, headWeight, eyeWeight, clampWeight;
	public Transform targetObjT;

	void OnAnimatorIK(){
		anim.SetLookAtWeight (lookWeight, bodyWeight, headWeight, eyeWeight, clampWeight);
		anim.SetLookAtPosition (targetObjT.position);
	}
}
