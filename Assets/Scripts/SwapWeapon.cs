using UnityEngine;
using System.Collections.Generic;

public class SwapWeapon : MonoBehaviour
{
	public AnimationClip WeaponAnimationClip;
	public KeyCode NextWeaponKeyCode;
	public string AOControllerSlotName;

	Animator animator;
	AnimatorOverrideController animatorOverrideController;

	int weaponIndex;

	public void Start()
	{
		animator = GetComponent<Animator>();
		weaponIndex = 0;

        //Create a AnimatorOverrideController at runtime, starting from our current AnimatorController
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animatorOverrideController.name = "runtimeAnimatorOverrideCtrl";
        //Change our current AnimatorController: now we are using the new AnimatorOverrideController, created at runtime
        animator.runtimeAnimatorController = animatorOverrideController;
    }

	public void Update()
	{
		if (Input.GetKeyDown(NextWeaponKeyCode))
		{
            //We can call also animatorOverrideController[AnimationClip]
            animatorOverrideController[AOControllerSlotName] = WeaponAnimationClip;
		}
	}
}