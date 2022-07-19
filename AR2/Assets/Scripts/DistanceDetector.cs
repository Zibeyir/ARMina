using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDetector : MonoBehaviour
{

    
    public GameObject InstanceObject;
    public Transform Plane;
    public float AreaInstanceSize=5;
    public GameObject DisctanceObject;
    Transform DisctanceObjectT;
    Transform InsctanceT;
    Transform ArCamera; 
    RaycastHit HitInfo;
    float distance;
    
    void Awake()
    {
        ArCamera = GetComponent<Transform>();
        DisctanceObjectT = DisctanceObject.transform;
        InsctanceT = Instantiate(InstanceObject, new Vector3(Random.Range(-AreaInstanceSize, AreaInstanceSize), Plane.position.y, Random.Range(-AreaInstanceSize, AreaInstanceSize)),Quaternion.identity).transform;
    }
    public float Distance(Vector3 v)
    {
        float x = 1 - (Vector3.Distance(v, InsctanceT.transform.position) / 21);
        if (x<=.2f)
        {
            x = .2f;

        }
        return x;
    }
    
    void FixedUpdate()
    {
        Transform cameraTransform = ArCamera.transform;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 100.0f))
        {
            if (HitInfo.transform.CompareTag("Plane")|| HitInfo.transform.CompareTag("Mina"))
            {
                distance = Distance(HitInfo.point);
                DisctanceObjectT.localScale = new Vector3(distance, distance, distance);
            }
            else
            {
                DisctanceObjectT.localScale = new Vector3(.2f, .2f, .2f);
            }
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);
        }
        

    }

    public void Select()
    {
        Transform cameraTransform = ArCamera.transform;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 100.0f))
        {

            if (HitInfo.transform.CompareTag("Mina"))
            {

                DisctanceObject.GetComponent<MeshRenderer>().material.color = Color.green;
                InsctanceT.position = new Vector3(Random.Range(-AreaInstanceSize, AreaInstanceSize), Plane.position.y, Random.Range(-AreaInstanceSize, AreaInstanceSize));
            }
            else
            {
                DisctanceObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        else
        {
            DisctanceObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
