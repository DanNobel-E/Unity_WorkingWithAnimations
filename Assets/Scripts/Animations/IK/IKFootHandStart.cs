using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//2nd Lesson on Feet IK - start point
public class IKFootHandStart : MonoBehaviour
{
    public bool MoveRootTransformY;
    [Header("Feet IK")]
    public bool FeetIK;
    public bool HandsIK;
    public bool FeetRotationIK;
    public bool HandsRotationIK;

    //If we want to move the body COM toward front leg if we are in IDLE and the slope is positive, backward otherwise, to add realism
    public bool MoveCOM;
    //The ray from the L/RFoot to the floor
    public float FootRayCastOffset = 0.3f;
    public float ShoulderRayCastOffset = 0.3f;

    //The foot should be placed where we hit the ground + FootOffset upward
    public Vector3 FootOffset = new Vector3(0, 0.1f, 0);
    public Vector3 HandsOffset = new Vector3(0, 0, -0.2f);

    //These helpers help us to calculate foot rotation
    public Transform LF_Helper, RF_Helper;
    public Transform LS_HelperMin, RS_HelperMin;
    public Transform LS_HelperMax, RS_HelperMax;

    public Transform LH_Helper, RH_Helper;


    public Vector3 LFootHelperOffset = new Vector3(0, 0, 0.5f);
    public Vector3 RFootHelperOffset = new Vector3(0, 0, 0.5f);

    public float ShouldersHelperMinOffset =0.4f;
    public float ShouldersHelperMaxOffset = 0.7f;

    public Vector3 LHandHelperOffset = new Vector3(-0.5f, 0, 0);
    public Vector3 RHandHelperOffset = new Vector3(0.5f, 0, 0);

    
    //Highlight the raycast hitting point for each foot
    public Transform LF_HitDebug, RF_HitDebug;
    public Transform LS_HitDebug, RS_HitDebug;

    public LayerMask FeetRaycastMask;
    public LayerMask ShouldersRaycastMask;

    //If MoveCOM == true, we'll move body COM a max amount of maxDistanceFromCOM, depending on maxOffsetBetweenFeet
    public float maxOffsetBetweenFeet = .5f, maxDistanceFromCOM = .2f;

    RaycastHit footHit;
    RaycastHit shoulderHit;

    Transform leftFoot, rightFoot;
    Transform leftShoulder, rightShoulder;
    Transform leftHand, rightHand;


    float lFootW, rFootW;
    float lHandW, rHandW;
    float lHandRW, rHandRW;


    //Final position and rotation
    Vector3 lfPos, rfPos;
    Vector3 lhPos, rhPos;

    Quaternion lfRot, rfRot;
    Quaternion lhRot, rhRot;


    Animator anim;
    //We'll move the root transform Y value depending on the raycast from the hips, downward
    Transform hips;
    bool isFalling;

    void Start()
    {
        anim = GetComponent<Animator>();
        hips = anim.GetBoneTransform(HumanBodyBones.Hips);

        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);
        LF_Helper.parent = leftFoot;
        LF_Helper.localPosition = LFootHelperOffset;
        RF_Helper.parent = rightFoot;
        RF_Helper.localPosition = RFootHelperOffset;

        leftShoulder = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        rightShoulder = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);

        Vector3 fwd = anim.gameObject.transform.forward;
        fwd.y = 0;
        fwd.Normalize();


        LS_HelperMin.position = leftShoulder.position + fwd*ShouldersHelperMinOffset;
        LS_HelperMin.rotation = Quaternion.LookRotation(fwd);
        LS_HelperMin.parent = anim.gameObject.transform;

        RS_HelperMin.position = rightShoulder.position + fwd * ShouldersHelperMinOffset;
        RS_HelperMin.rotation = Quaternion.LookRotation(fwd);
        RS_HelperMin.parent = anim.gameObject.transform;

        LS_HelperMax.position = leftShoulder.position + fwd * ShouldersHelperMaxOffset;
        LS_HelperMax.rotation = Quaternion.LookRotation(fwd);
        LS_HelperMax.parent = anim.gameObject.transform;

        RS_HelperMax.position = rightShoulder.position + fwd * ShouldersHelperMaxOffset;
        RS_HelperMax.rotation = Quaternion.LookRotation(fwd);
        RS_HelperMax.parent = anim.gameObject.transform;



        leftHand = anim.GetBoneTransform(HumanBodyBones.LeftHand);
        rightHand = anim.GetBoneTransform(HumanBodyBones.RightHand);
        LH_Helper.parent = leftHand;
        LH_Helper.localPosition = LHandHelperOffset;
        RH_Helper.parent = rightHand;
        RH_Helper.localPosition = RHandHelperOffset;

        
        

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (anim == null)
            return;

        if (MoveRootTransformY)
        {
            FindBodyPosition();
        }
        //Adjust RootTransform.y

        if (isFalling)
            return;

        if (FeetIK)
        {
            FindFootPosition(leftFoot, LF_Helper, ref lfPos, ref lfRot);
            FindFootPosition(rightFoot, RF_Helper, ref rfPos, ref rfRot);

            //Set Weight
            lFootW = anim.GetFloat("LeftFoot");
            rFootW = anim.GetFloat("RightFoot");

            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lFootW);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rFootW);

            anim.SetIKPosition(AvatarIKGoal.LeftFoot, lfPos);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, rfPos);

            if (FeetRotationIK)
            {

                anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

                anim.SetIKRotation(AvatarIKGoal.LeftFoot, lfRot);
                anim.SetIKRotation(AvatarIKGoal.RightFoot, rfRot);


            }






        }


        if (HandsIK)
        {
            if (lHandW == 1 && rHandW == 1)
            {

                anim.SetBool("Push", true);
            }
            else
            {
                anim.SetBool("Push", false);

            }


            FindHandPosition(leftShoulder, LS_HelperMax, LS_HelperMin, ref lhPos, ref lhRot, ref lHandW, ref lHandRW);
            FindHandPosition(rightShoulder, RS_HelperMax, RS_HelperMin, ref rhPos, ref rhRot, ref rHandW, ref rHandRW);

            //Set Weight

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, lHandW);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand,rHandW);

            anim.SetIKPosition(AvatarIKGoal.LeftHand, lhPos);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rhPos);

            if (HandsRotationIK)
            {

                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, lHandRW);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rHandRW);

                anim.SetIKRotation(AvatarIKGoal.LeftHand, lhRot);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rhRot);


            }

            

        }


        //Adjust anim.bodyPosition
        if (MoveCOM)
        {
            float leftOffset, rightOffset;
            leftOffset = lfPos.y - transform.position.y;
            rightOffset = rfPos.y - transform.position.y;
            float min = Mathf.Min(Mathf.Abs(leftOffset - rightOffset), maxOffsetBetweenFeet);
            float fraction = min / maxOffsetBetweenFeet;
            fraction = leftOffset < rightOffset ? fraction *= -1 : fraction;
            anim.bodyPosition += (fraction * maxDistanceFromCOM) * transform.forward;
        }
    }

    void FindFootPosition(Transform t, Transform t_helper, ref Vector3 targetPosition, ref Quaternion targetRotation)
    {
        Vector3 origin = t.position + Vector3.up * FootRayCastOffset;
        Debug.DrawRay(origin, Vector3.down, Color.yellow);
        if (Physics.Raycast(origin, Vector3.down, out footHit, 1, FeetRaycastMask))
        {
            targetPosition = footHit.point + FootOffset;

            if (FeetRotationIK)
            {
                Vector3 dir = t_helper.position - t.position;
                dir.y = 0;
                Quaternion rot = Quaternion.LookRotation(dir);
                targetRotation = Quaternion.FromToRotation(Vector3.up, footHit.normal) * rot;
            }
        }




    }

    void FindHandPosition(Transform t, Transform t_helperMax, Transform t_helperMin, ref Vector3 targetPosition, ref Quaternion targetRotation, ref float tWeight, ref float tRWeight)
    {
       

        
        Vector3 origin = t.position;
        Debug.DrawRay(origin, t_helperMax.forward, Color.yellow);
        tWeight = 0;
        tRWeight = 0;
        if (Physics.Raycast(origin, t_helperMax.forward, out shoulderHit, 5, ShouldersRaycastMask))
        {
            targetPosition = shoulderHit.point + HandsOffset;
            float distToTarget = (targetPosition - t.position).magnitude;
            float distToMaxHandler= (t_helperMax.position - t.position).magnitude;
            float distToMinHandler = (t_helperMin.position - t.position).magnitude;
            if (distToTarget <= distToMaxHandler)
            {
                float offsetDist = (t_helperMin.position - t_helperMax.position).magnitude;
                tWeight = 1 - (distToTarget-distToMinHandler) / offsetDist;

                if (tWeight > 1)
                {
                    tWeight = 1;
                }

                if (HandsRotationIK)
                {
                    tRWeight = tWeight;
                    Vector3 dir = t_helperMax.position - t.position;
                    dir.y = 0;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    targetRotation = Quaternion.FromToRotation(Vector3.up, shoulderHit.normal) * rot;
                }


            }

        }




    }

    void FindBodyPosition()
    {
        RaycastHit hit;
        Vector3 origin = hips.position;

        Debug.DrawRay(origin, Vector3.down * 2, Color.yellow);
        if (Physics.Raycast(origin, Vector3.down, out hit, 10, FeetRaycastMask))
        {
            if (Vector3.Distance(hit.point, transform.position) > 2)
            {
                isFalling = true;
            }
            else
            {
                isFalling = false;
            }

            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
        else
        {
            isFalling = true;
        }
    }

    void Update()
    {
        
    }
}

