using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comboSMBInit : MonoBehaviour {
    public GameObject comboHighlight;
    public GameObject comboFailedHighlight;
    public GameObject comboSucceedHighlight;

    public Animator anim;
    comboSMB[] comboSMBs;

    void Start()
    {
        //Lazy Initialization
        comboSMBs = anim.GetBehaviours<comboSMB>();
        foreach (comboSMB currSMB in comboSMBs)
        {
            currSMB.comboHighlight = comboHighlight;
            currSMB.comboFailedHighlight = comboFailedHighlight;
            currSMB.comboSucceedHighlight = comboSucceedHighlight;
        }
    }

}
