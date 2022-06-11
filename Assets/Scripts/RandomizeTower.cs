using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTower : MonoBehaviour
{
    public GameObject Prefab;
    public int Iterations;
    public bool Randomize;
    public Transform Root;
    public float Offset=0;

    Vector3 prevPosition;
    float height=0;
    float xScale;
    float zScale;
    float xOffset;
    float zOffset;
    float rot=0;

    // Start is called before the first frame update
    void Start()
    {
        height = Prefab.transform.localScale.y*2;
        xScale= Prefab.transform.localScale.x ;
        zScale = Prefab.transform.localScale.z*2 ;

        Root.position = new Vector3(Root.position.x, Root.position.y + height/2, Root.position.z);

        for (int i = 0; i < Iterations; i++)
        {
            
            Vector3 newPos = new Vector3(Root.position.x+xOffset, Root.position.y +(i*height)+(i*Offset), Root.position.z+zOffset);

            GameObject gO= Instantiate(Prefab, newPos, Root.rotation, Root);

            gO.transform.Rotate(0, rot, 0);

            if (Randomize)
            {
                int randomPos = Random.Range(0, 3);
                int randomRot = Random.Range(0, 3);


                switch (randomRot)
                {
                    case 0:
                        {
                            rot=0;
                        }
                        break;
                    case 1:
                        {
                           rot=90;

                        }
                        break;
                    case 2:
                        {
                            rot = -90;

                        }
                        break;
                }

                switch (randomPos)
                {
                    case 0:
                        {
                            xOffset += xScale;
                        }
                        break;
                    case 1:
                        {
                            xOffset -= xScale;

                        }
                        break;
                    case 2:
                        {
                            zOffset += zScale;

                        }
                        break;
                }

            }

        }
    }

}
