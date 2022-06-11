using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKMB : MonoBehaviour
{
    public Transform goal;
    public Transform effector;
    public Transform baseBone;
    public float sqrDistError = 0.01f;
    [Range(0f, 1f)]
    public float weight = 1f;
    public int maxIterationCount = 10;

    List<Transform> m_Bones;

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

    //This happens after the animator has done its stuff in update
    void LateUpdate ()
    {
        Solve ();
    }
    
    void Solve ()
    {
        Vector3 goalPosition = goal.position;
        Vector3 effectorPosition = m_Bones[0].position;

        Vector3 targetPosition = Vector3.Lerp (effectorPosition, goalPosition, weight);
        float sqrDistance;

        int iterationCount = 0;
        do
        {

            for (int i = 0; i < m_Bones.Count - 2; i++)
            {
                for (int j = 1; j < i + 3 && j < m_Bones.Count; j++)
                {
                    //0,1 - 0,1,2 - 0,1,2,3 - etc...
                    RotateBone(m_Bones[0], m_Bones[j], targetPosition);
                    
                    sqrDistance = (m_Bones[0].position - targetPosition).sqrMagnitude;
                    
                    if(sqrDistance <= sqrDistError)
                        return;
                }
            }
            
            sqrDistance = (m_Bones[0].position - targetPosition).sqrMagnitude;
            iterationCount++;
        }
        while (sqrDistance > sqrDistError && iterationCount <= maxIterationCount);
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
