using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnJumpTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<RemyController>().SetJumpTrigger();
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<RemyController>().SetJumpTrigger();

    }
}
