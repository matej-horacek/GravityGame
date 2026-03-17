using UnityEngine;

public class CameraGravityAnchor : MonoBehaviour
{
    public Transform playerBody;
    private Vector3 prevGravityUp;
    private Vector3 prevBodyUp;
    private void Awake()
    {
        prevGravityUp = -Physics.gravity.normalized;
        prevBodyUp = transform.up;

    }

    void LateUpdate()
    {
        // 1. Follow the player's position perfectly
        transform.position = playerBody.position;

        // 2. Find the world's new "Up" and our current "Up"
        Vector3 gravityUp = -Physics.gravity.normalized;
        Vector3 currentUp = transform.up;

        // 3. ONLY change rotation if gravity has shifted
        if (gravityUp != prevGravityUp)
        {
            Debug.Log("GravityAnchor called");


            if (gravityUp.y < -0.001f) 
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 180f);
                Debug.Log("ceiling");
            }
            else 
            {
                Quaternion shortestTilt = Quaternion.FromToRotation(currentUp, gravityUp);
                transform.rotation = shortestTilt * transform.rotation;

            }
            prevBodyUp = currentUp;
            prevGravityUp = gravityUp;
            // Floor-to-Ceiling Check (180 degrees)
            /*if (Vector3.Dot(currentUp, gravityUp) < -0.99f)
            {
                // FORCE an instant local barrel roll (Z-axis). No Y-axis flipping allowed.
                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 180f);
            }
            else
            {
                // Wall Check (90 degrees or diagonal)
                // Calculate the exact shortest tilt from the old Up to the new Up
                Quaternion shortestTilt = Quaternion.FromToRotation(currentUp, gravityUp);

                // Apply that tilt instantly to our current rotation
                transform.rotation = shortestTilt * transform.rotation;
            }*/
        }
    }
}