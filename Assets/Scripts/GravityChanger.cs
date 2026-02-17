using System;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    [SerializeField]  float GravityForce= 5f;
    //[SerializeField] Transform firstPersonCamera;

    private Vector3 previousDirection;
    private float CollisionTimer = 0f;
    public float changeDelay = 0.2f;
    private Vector3 GetDirection(string tag) 
    {
        Vector3 gravitydirection;
        if (tag == "Floor")
            gravitydirection = new Vector3(0, (-1f * GravityForce), 0);
        else if (tag == "Roof")
            gravitydirection = new Vector3(0, (1f * GravityForce), 0);
        else if (tag == "LWall")
            gravitydirection = new Vector3((-1f * GravityForce), 0, 0);
        else if (tag == "RWall")
            gravitydirection = new Vector3((1f * GravityForce), 0, 0);
        else if (tag == "Back")
            gravitydirection = new Vector3(0, 0, (1f * GravityForce));
        else
            gravitydirection = new Vector3(0, (-1f * GravityForce), 0);
        return gravitydirection;
    }
    void Awake()
    {
        // Assume we start on the floor
        previousDirection = new Vector3(0, -GravityForce, 0);
    }
    
    private void setRotation(Vector3 newGravityDirection)
    {
        Vector3 oldUp = -previousDirection.normalized;
        Vector3 newUp = -newGravityDirection.normalized;

        //Debug.Log("Old" + transform.position);
        if (newGravityDirection.y <= -1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //firstPersonCamera.rotation = transform.rotation;
            //Debug.Log("floor reset");
            transform.position += oldUp * 2f;
            return;
        }
        else if(previousDirection.x >= 1f && newGravityDirection.x <= -1f) 
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            //firstPersonCamera.rotation = transform.rotation;
            //Debug.Log("left to right");
            transform.position += oldUp * 2f;
            return;
        }
        else if(previousDirection.x <= -1f && newGravityDirection.x >= 1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            //firstPersonCamera.rotation = transform.rotation;
            //Debug.Log("right to left");
            transform.position += oldUp * 2f;
            return;
        }

        Quaternion rotationChange = Quaternion.FromToRotation(oldUp, newUp);
          transform.rotation = rotationChange * transform.rotation;
          //firstPersonCamera.rotation = transform.rotation;
        transform.position += oldUp * 2f;
        // Debug.Log( " New"+transform.position);
        // Optional: If you find the rotation drifts slightly over time, 
        // you might want to round the angles to the nearest 90 here.
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 newGravity = GetDirection(other.collider.tag);

        if (newGravity != previousDirection &&Time.time > CollisionTimer)
        {
            Physics.gravity = newGravity;
            setRotation(newGravity); // This now uses the math above
            previousDirection = newGravity;
            //Debug.Log("Gravity changed to " + Physics.gravity);

            CollisionTimer = Time.time + changeDelay;
        }
    }

}