using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpDown : MonoBehaviour
{

    public Animator Anim;
    public string UpLayerName;
    public string DownLayerName;
    public float Duration=1;

    public KeyCode UpKey, DownKey;
    int upLayerIndex;
    int downLayerIndex;
    float timerUp=0;
    float timerDown = 0;
    bool playTimerUp =false;
    bool playTimerDown = false;

    bool revertTimerUp = false;
    bool revertTimerDown = false;



    // Start is called before the first frame update
    void Start()
    {
       upLayerIndex=  Anim.GetLayerIndex(UpLayerName);
       downLayerIndex = Anim.GetLayerIndex(DownLayerName);


    }

    // Update is called once per frame
    void Update()
    {
        TimerTick(ref timerUp ,ref playTimerUp);
        TimerTick(ref timerDown, ref playTimerDown);

        ReverseTimerTick(ref timerUp, ref revertTimerUp);
        ReverseTimerTick(ref timerDown, ref revertTimerDown);



        if (Input.GetKeyDown(UpKey))
        {
            playTimerUp = true;
            timerUp = 0;
        }
        if (Input.GetKeyUp(UpKey))
        {
            revertTimerUp = true;
            timerUp = 1;
        }

        if (Input.GetKeyDown(DownKey))
        {
            playTimerDown = true;
            timerDown = 0;
        }
        if (Input.GetKeyUp(DownKey))
        {
            revertTimerDown = true;
            timerDown = 1;
        }

            Anim.SetLayerWeight(upLayerIndex, timerUp);
            Anim.SetLayerWeight(downLayerIndex, timerDown);
    }

    void TimerTick(ref float timer, ref bool timing)
    {
        if (timing)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                timing = false;
                timer = 1;
            }

        }
    }

    void ReverseTimerTick(ref float timer, ref bool timing)
    {

        if (timing)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timing = false;
                timer = 0;
            }

        }
    }
}
