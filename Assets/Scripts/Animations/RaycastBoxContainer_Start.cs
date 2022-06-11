using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBoxContainer_Start : MonoBehaviour
{
    public Transform RayStart;
    public float RayLen;
    public Transform HitDebugT;
    public Transform ColliderDebugT;
    public bool CastRaycast = true;
    bool Hitted = false;
    public GameObject prefab;

    RaycastHit raycastHit;
    Ray ray;
    Collider currCollider;
    GameObject currGO;
    Vector3[] vertices;
    Vector3[] objFacesCenters=new Vector3[6];
    float distToNearestVertex = 0;
    Vector3 nearestVertex=Vector3.zero;
    Vector3 MTargetPos;


    void Update()
    {
        if (!CastRaycast)
        {
            return;
        }


        Debug.DrawLine(RayStart.position, RayStart.position + transform.forward * (RayLen == 0 ? 100 : RayLen), Color.yellow);

        ray = new Ray(RayStart.position, transform.forward);

        if (RayLen != 0)
            Hitted = Physics.Raycast(ray, out raycastHit);
        else
            Hitted = Physics.Raycast(ray, out raycastHit);

        if (Hitted)
        {
            currCollider = raycastHit.collider;
            GameObject gO = currCollider.gameObject;


            if (currGO == null || currGO != gO)
            {
                currGO = gO;
                vertices = currGO.GetComponent<MeshFilter>().mesh.vertices;
                objFacesCenters[0] = currCollider.bounds.center + (currGO.transform.forward.normalized * gO.transform.localScale.z);
                objFacesCenters[1] = currCollider.bounds.center + (-currGO.transform.forward.normalized * gO.transform.localScale.z);
                
                objFacesCenters[2] = currCollider.bounds.center + (currGO.transform.right.normalized * gO.transform.localScale.x);
                objFacesCenters[3] = currCollider.bounds.center + (-currGO.transform.right.normalized * gO.transform.localScale.x);
                
                objFacesCenters[4] = currCollider.bounds.center + (currGO.transform.up.normalized * gO.transform.localScale.y);
                objFacesCenters[5] = currCollider.bounds.center + (-currGO.transform.up.normalized * gO.transform.localScale.y);

            }

            Vector3 faceCenterPos=objFacesCenters[0];

            for (int i = 0; i < objFacesCenters.Length; i++)
            {
                Vector3 faceDir = objFacesCenters[i] - currCollider.bounds.center;
                if(Vector3.Angle(faceDir.normalized, raycastHit.normal.normalized) == 0)
                {
                    faceCenterPos = objFacesCenters[i];
                }
            }


            

            Vector3 upPos = faceCenterPos + (Vector3.up * currCollider.bounds.extents.y);
            Vector3 downPos = faceCenterPos + (-Vector3.up * currCollider.bounds.extents.y);






            float currNearestDist = 0;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertexPos = currGO.transform.TransformPoint(vertices[i]);
                Vector3 dist = raycastHit.point - vertexPos;
                Debug.DrawLine(vertexPos, raycastHit.point, Color.red);
                float distToHitPoint = dist.magnitude;

                if (currNearestDist == 0 || distToHitPoint < currNearestDist)
                {
                    currNearestDist = distToHitPoint;
                    nearestVertex = vertexPos;
                }


            }

            distToNearestVertex = currNearestDist;
           
            

            if ((upPos - raycastHit.point).magnitude < (downPos - raycastHit.point).magnitude)
            {
                Vector3 distToUp = upPos - nearestVertex;
                Vector3 distToPoint = raycastHit.point - nearestVertex;
                Vector3 projection = Vector3.Project(distToPoint, distToUp.normalized);
                MTargetPos = nearestVertex + projection;
            }
            else
            {
                Vector3 distToDown = downPos - nearestVertex;
                Vector3 distToPoint = raycastHit.point - nearestVertex;
                Vector3 projection = Vector3.Project(distToPoint, distToDown.normalized);
                MTargetPos = nearestVertex + projection + (Vector3.up.normalized*currCollider.bounds.size.y);

                Debug.DrawLine(nearestVertex, nearestVertex + projection, Color.green);
            }

            //Instantiate(prefab, faceCenterPos, Quaternion.LookRotation(raycastHit.normal), currGO.transform);
            //Instantiate(prefab, upPos, Quaternion.LookRotation(raycastHit.normal), currGO.transform);
            //Instantiate(prefab, downPos, Quaternion.LookRotation(raycastHit.normal), currGO.transform);
            //Instantiate(prefab, nearestVertex, Quaternion.LookRotation(raycastHit.normal), currGO.transform);



            HitDebugT.position = raycastHit.point;
            ColliderDebugT.position = MTargetPos;
            ColliderDebugT.rotation = Quaternion.LookRotation(-raycastHit.normal);

           

        }
    }
}
