using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivate : MonoBehaviour
{
    
    public string TriggerName;
    // Start is called before the first frame update
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Animator>().SetTrigger(TriggerName);
    }
}
