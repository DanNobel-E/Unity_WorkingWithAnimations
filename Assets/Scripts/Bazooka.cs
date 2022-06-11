using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    public Transform Target;
    public Transform LeftHandler, RightHandler;
    public GameObject Bazoo;
    public GameObject BulletPrefab;
    public Transform SpawnPoint;
    public KeyCode FireKeyCode;
    public string bazookaLayer = "Bazooka";

    public float RecoilDuration=0.5f;

    Animator anim;
    int bazooLayerIndex;
    float timer = 0;
   


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        bazooLayerIndex= anim.GetLayerIndex(bazookaLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(FireKeyCode))
        {

            Instantiate(BulletPrefab, SpawnPoint.position, SpawnPoint.rotation);
            
            anim.SetFloat("Fire", 1);

            timer = RecoilDuration;
            

        }

        if (anim.GetFloat("Fire") > 0)
        {

            timer -= Time.deltaTime;

            float percentage = timer / RecoilDuration;
            anim.SetFloat("Fire", percentage);

            if (timer <= 0)
            {
                
                anim.SetFloat("Fire", 0);
            }
        }


    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (layerIndex < 2)
        {
            anim.SetLookAtWeight(1, 0.8f,0.8f, 0.8f, 1);
            anim.SetLookAtPosition(Target.position);

            anim.SetLayerWeight(bazooLayerIndex, anim.GetFloat("Fire"));

        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandler.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandler.position);


            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotation(AvatarIKGoal.RightHand, RightHandler.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandler.rotation);

        }


    }
}
