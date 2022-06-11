using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float LifeDuration=4;

    public float Speed = 10;

    Rigidbody rb;

    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward* Speed;

        CancelInvoke();
        Invoke("Hide", LifeDuration);
    }



    //private void Update()
    //{
    //    transform.Translate(0, 0, Speed * Time.deltaTime);
    //}

    void Hide()
    {
        Destroy(gameObject);
    }
}
