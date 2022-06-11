using UnityEngine;
using System.Collections.Generic;

//AnimationClipOverrides is a List<KeyValuePair<AnimationClip, AnimationClip>> with an Indexer
public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}

[System.Serializable]
public class Weapon
{
    public AnimationClip singleAttack;
    public AnimationClip comboAttack;
    public AnimationClip dashAttack;
}

public class SwapMultipleWeapons : MonoBehaviour
{
    public Weapon[] weapons;
    public KeyCode NextWeaponKeyCode;
    public string SingleAttackStateName = "HumanoidWalk";
    public string ComboAttackStateName = "HumanoidRun";
    public string DashAttackStateName = "JumpNotInPlace_Corrected";

    Animator animator;
    AnimatorOverrideController animatorOverrideController;
    int weaponIndex;
    AnimationClipOverrides clipOverrides;

    public void Start()
    {
        animator = GetComponent<Animator>();
        weaponIndex = 0;

        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController.name = "runtimeAnimatorOverrideCtrl";

        //Init clipOverrides with an empty List of capacity animatorOverrideController.overridesCount
        clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(clipOverrides);
        //This is a NULL exception: clipOverrides now is a list of Keypairs with this kind of content: <Keypair(AClip, Empty), Keypair(AClip, Empty), ... >
        // clipOverrides["JumpNotInPlace_Corrected"] asks for the AnimationClip coupled with the AnimationClip "JumpNotInPlace_Corrected". And that couple is:
        // Keypair(AClip"JumpNotInPlace_Corrected", Empty)
        AnimationClip a = clipOverrides["JumpNotInPlace_Corrected"];
        //Debug.Log("TEST: " + a.name);
    }

    public void Update()
    {
        if (Input.GetKeyDown(NextWeaponKeyCode))
        {
            //We have to fill the second value in each clipOverrides clipOverrides keyvaluepairs. After the first NextWeaponButton, clipOverrides will become:
            //  <Keypair(AClip, new_AClip), Keypair(AClip, new_AClip), ... >
            Debug.Log("ChangingAnimationClips");
            weaponIndex = (weaponIndex + 1) % weapons.Length;
            clipOverrides[SingleAttackStateName] = weapons[weaponIndex].singleAttack;
            clipOverrides[ComboAttackStateName] = weapons[weaponIndex].comboAttack;
            clipOverrides[DashAttackStateName] = weapons[weaponIndex].dashAttack;
            Debug.Log(clipOverrides[DashAttackStateName]);
            animatorOverrideController.ApplyOverrides(clipOverrides);
        }
    }
}