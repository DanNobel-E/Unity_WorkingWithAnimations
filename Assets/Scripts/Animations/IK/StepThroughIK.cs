using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepThroughIK : MonoBehaviour
{
    public Transform goal;
    public Transform effector;
    public Transform baseBone;
    public bool StepByStep = true;

    List<Transform> m_Bones;
    int m_BoneIndex;

    void OnEnable ()
    {
        // Create a list of bones starting at the effector.
        m_Bones = new List<Transform> ();
        Transform current = effector;
		
        // Add bones up the hierarchy until the base bone is reached.
        while (current != null && current != baseBone.parent)
        {
            m_Bones.Add (current);
            current = current.parent;
        }
        if(current == null)
            throw new UnityException("Base Bone is not an ancestror of Effector.  IK will fail.");
    }

    void LateUpdate ()
    {
        if(StepByStep && Input.GetKeyDown (KeyCode.Space))
            Step ();
        if(!StepByStep)
            Step();
    }

    void Step ()
    {
        m_BoneIndex++;
        if (m_BoneIndex == m_Bones.Count)
            m_BoneIndex = 1;
        
        
        RotateBone (m_Bones[0], m_Bones[m_BoneIndex], goal.position);
    }

    public static void RotateBone (Transform effector, Transform bone, Vector3 goalPosition)
    {
        Vector3 effectorPosition = effector.position;
        Vector3 bonePosition = bone.position;
        Quaternion boneRotation = bone.rotation;
        
        Vector3 boneToEffector = effectorPosition - bonePosition;
        Vector3 boneToGoal = goalPosition - bonePosition;
        
        Quaternion fromToRotation = Quaternion.FromToRotation (boneToEffector, boneToGoal);
        Quaternion newRotation = fromToRotation * boneRotation;
        
        bone.rotation = newRotation;
    }
}
