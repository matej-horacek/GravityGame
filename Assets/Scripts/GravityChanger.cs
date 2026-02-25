using System;
using Unity.Cinemachine;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    [SerializeField] Transform firstPersonCamera;
    [SerializeField] CinemachineBrain cmBrain;
    //[SerializeField] Transform firstPersonCamera;

    private Vector3 previousPullDirection;
    private float CollisionTimer = 0f;
    public float changeDelay = 0.2f;
    
    void Awake()
    {
        //first load of gravity
        previousPullDirection = Physics.gravity.normalized;
    }
    private void OnCollisionEnter(Collision other)
    {
        // inverted up direction of the collided object
        Vector3 newGravity = GetPullDirection(other);

        //
        if (newGravity != previousPullDirection && Time.time > CollisionTimer)
        {
            Physics.gravity = newGravity;
            setRotation(newGravity);
            previousPullDirection = newGravity;
            //Debug.Log("Gravity changed to " + Physics.gravity);

            CollisionTimer = Time.time + changeDelay;
        }
    }
    private Vector3 GetPullDirection(Collision other)
    {
        Vector3 gravityPull = Physics.gravity.normalized;
        if (other != null && other.transform.up.normalized != -Physics.gravity.normalized)
        {
            gravityPull = -other.transform.up;
            //Debug.Log("Gravity pull is " + gravityPull);
        }
        return gravityPull;



    }

    private void setRotation(Vector3 newPullDirection)
    {
        Vector3 oldDown = previousPullDirection;
        Vector3 newDown = newPullDirection.normalized;

        Quaternion rotationDelta;

        // Vector3.Dot measures how similar two directions are. -1 means exactly opposite.
        if (Vector3.Dot(oldDown, newDown) < -0.99f)
        {
            // FromToRotation doesn't know which way to spin a 180-degree turn, 
            // so we explicitly tell it to do a backflip around the player's forward axis.
            rotationDelta = Quaternion.AngleAxis(180f, transform.forward);
        }
        else
        {
            // STANDARD CASE: Calculates the dynamic rotation for ANY angle (90 degrees, ramps, etc.)
            rotationDelta = Quaternion.FromToRotation(oldDown, newDown);
        }

        // Apply the rotation
        if(cmBrain.ActiveVirtualCamera.Name == "FirstPersonCamera") 
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAA");
            firstPersonCamera.rotation = rotationDelta * transform.rotation;
        }
            
        transform.rotation = rotationDelta * transform.rotation;

        // Move the player away from the old surface so they don't clip through the mesh.
        transform.position += -oldDown * 2f;

    }
    

}