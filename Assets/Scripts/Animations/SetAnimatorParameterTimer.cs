using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorParameterTimer : MonoBehaviour
{
    public Animator anim;
    public string ParamName;
    public float ParamOnFloatVal, ParamOffFloatVal;
    public float duration;
    public bool start;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (start && timer>0)
            start = false;
        else if (start)
        {
            start = false;
            timer = duration;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            anim.SetFloat(ParamName, ParamOnFloatVal);
        }
        else
            anim.SetFloat(ParamName, ParamOffFloatVal);
    }
}
