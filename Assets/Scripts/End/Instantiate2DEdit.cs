using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Instantiate2DEdit : MonoBehaviour
{
    public bool instantiateN, destoryAll;
    public Transform parent;
    public int nx, nz;
    public float offsetX, offsetZ;
    public GameObject prefab;

    Vector3 startT, currT;

    void Instantiate()
    {
        startT = Vector3.zero;
        currT = Vector3.zero;
        for (int i = 0; i < nx; i++)
        {
            for (int j = 0; j < nz; j++)
            {
                GameObject go;
                if (parent != null)
                    go = Instantiate(prefab, parent);
                else
                    go = Instantiate(prefab);
                go.transform.localPosition = currT;
                currT = new Vector3(currT.x + offsetX, currT.y, currT.z);
            }
            currT = new Vector3(startT.x, currT.y, currT.z + offsetZ);
        }
    }

    private void Update()
    {
        if (instantiateN)
        {
            instantiateN = false;
            Instantiate();
        }
        if (destoryAll)
        {
            destoryAll = false;
            DestroyAll();
        }
    }
    void DestroyAll()
    {
        int n = transform.childCount;
        List<Transform> children = new List<Transform>();
        for (int i = n - 1; i >= 0; i--)
        {
            if (Application.isPlaying)
                Destroy(transform.GetChild(i).gameObject);
            else
                DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
