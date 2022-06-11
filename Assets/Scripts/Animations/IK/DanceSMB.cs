using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceSMB : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Do a sine based on the State normalized time
        // A: Mathf.Repeat(stateInfo.normalizedTime, 1.0f) Goes from 0 to 1
        // B: A * Mathf.PI Goes from 0 to PI radians
        // C: Sin(B) Goes from 0 (at start) to 1 (PI/2) to 0 again (PI)
        // D: (1 + C) * 0.5 Goes from 0 to 1
        float speed = 4.0f;
		float normalizedTime = (1.0f + Mathf.Sin(Mathf.Repeat(stateInfo.normalizedTime, 1.0f) * Mathf.PI * speed)) * 0.5f;

		// lower the body
		animator.bodyPosition = animator.bodyPosition + new Vector3(0, -0.3f, 0);

		// make hands move left and right based on sine
		Vector3 leftHandPosition = animator.bodyPosition + new Vector3(-0.1f + normalizedTime * -0.2f, 0, 0.4f);
		Vector3 rightHandPosition = animator.bodyPosition + new Vector3(0.5f + normalizedTime * 0.2f, -0.6f, -0.1f);

		// put elbow up
		Vector3 leftElbowPosition = animator.bodyPosition + new Vector3(-5, 2, 0);
		Vector3 rightElbowPosition = animator.bodyPosition + new Vector3(5, 2, 0);

		animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPosition);
		animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPosition);

		// align knees to the hand
		animator.SetIKHintPosition(AvatarIKHint.LeftKnee, leftHandPosition);
		animator.SetIKHintPosition(AvatarIKHint.RightKnee, rightHandPosition);
		animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowPosition);
		animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowPosition);

		// activate everything. Could be done on start
		animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
		animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);
		animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);
		animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1.0f);
		animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
		animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
		animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 1.0f);
		animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 1.0f);
		animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1.0f);
		animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1.0f);
	}
}
