using UnityEngine;
using System.Collections.Generic;

public class SwapAttack : MonoBehaviour
{
	public AnimationClip[] WeaponAnimationClips;
	public string OverrideClipName="Rifle_ShootBurst";

	public string[] WeaponAnimationClipsNames;
	AnimationClip overrideClip;

	public KeyCode NextWeaponKeyCode;


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

		

		overrideClip = animatorOverrideController[OverrideClipName];
    }

	public void Update()
	{
		if (Input.GetKeyDown(NextWeaponKeyCode))
		{

            if (weaponIndex >= WeaponAnimationClips.Length)
            {
				weaponIndex = 0;
            }

            //We can call also animatorOverrideController[AnimationClip]
            animatorOverrideController[OverrideClipName] = WeaponAnimationClips[weaponIndex];
			weaponIndex++;
		}
	}

    public void OnGUI()
    {
		GUI.Box(new Rect(10, 10, 120, 60), GUIContent.none);
		GUI.Label(new Rect(20, 20, 100, 20), "Current Weapon:");
		GUI.Label(new Rect(20, 40, 100, 20), WeaponAnimationClipsNames[weaponIndex-1]);

	}
}