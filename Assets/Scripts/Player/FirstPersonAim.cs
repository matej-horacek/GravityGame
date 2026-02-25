using UnityEngine;
using UnityEngine.InputSystem;

// 1. This tag forces this script to run AFTER Cinemachine! No more frame fighting.
public class FirstPersonAim : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    public float sensitivity = 0.1f;
    [SerializeField] Rigidbody rb;
    private float pendingRotation = 0f;

    void Start()
    {
        // Good practice to ensure physics don't knock your player over
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 1. READ INPUT HERE
        // Update runs every single rendered frame, so it catches every tiny mouse movement.
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        // Add the movement to our pending rotation
        pendingRotation += mouseDelta.x * sensitivity;
    }

    void FixedUpdate()
    {
        // 2. APPLY ROTATION HERE
        // FixedUpdate runs in sync with the physics engine.
        if (pendingRotation != 0f)
        {
            // Calculate the new rotation
            Quaternion deltaRotation = Quaternion.Euler(0f, pendingRotation, 0f);

            // Use MoveRotation instead of transform.Rotate to play nice with physics
            rb.MoveRotation(rb.rotation * deltaRotation);

            // Reset to zero so we don't keep spinning!
            pendingRotation = 0f;
        }
    }
}