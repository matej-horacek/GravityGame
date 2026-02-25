using UnityEngine;

// 1. This tag forces this script to run AFTER Cinemachine! No more frame fighting.
[DefaultExecutionOrder(100)]
public class FirstPersonAim : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;

    private float turnSpeed = 1500f;

    void LateUpdate()
    {
        // Get the direction the camera is looking
        Vector3 camForward = cameraTransform.forward;

        // Flatten that direction against the Player's current gravity
        Vector3 flattenedForward = Vector3.ProjectOnPlane(camForward, transform.up).normalized;

        if (flattenedForward.sqrMagnitude > 0.001f)
        {
            // Instant snap! No Slerp delay, meaning the camera can never clip into the body.
            // The [DefaultExecutionOrder] at the top of the script prevents the stuttering.
            transform.rotation = Quaternion.LookRotation(flattenedForward, transform.up);
        }
    }
}