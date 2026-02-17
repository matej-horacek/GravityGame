using UnityEngine;
using Unity.Cinemachine;
using System;
using UnityEngine.Rendering;

public class FirstPersonAim : MonoBehaviour
{
    [SerializeField] Transform cameraTransform; //Main Camera here
    [SerializeField] Transform eyes;

    void LateUpdate()
    {
        
        Vector3 targetRotation = cameraTransform.eulerAngles;
        Vector3 UpDirection = new Vector3(
            Mathf.Abs(Physics.gravity.x),
            Mathf.Abs(Physics.gravity.y),
            Mathf.Abs(Physics.gravity.z)
        );
        if(UpDirection.y > 0) 
        {
            eyes.localRotation = Quaternion.Euler(targetRotation.x,0,targetRotation.z);
            targetRotation.x= 0;
            targetRotation.z= 0;

            Debug.Log("yRotation");
        }
        else if (UpDirection.x > 0) 
        {
            eyes.localRotation = Quaternion.Euler(0,targetRotation.y, targetRotation.z);
            targetRotation.y= 0;
            targetRotation.z= 0;
            Debug.Log("xRotation");
        }
        else if (UpDirection.z > 0)
        {
            eyes.localRotation = Quaternion.Euler(targetRotation.x, targetRotation.y, 0);
            targetRotation.x= 0;
            targetRotation.y= 0;
            Debug.Log("zRotation");
        }
        transform.rotation = Quaternion.Euler(targetRotation);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * 10f);
        /*    targetRotation = new Vector3(targetRotation.x, targetRotation.y, targetRotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * 10f);*/
    }
}
