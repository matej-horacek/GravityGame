using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    private Transform playerTarget;
    [SerializeField] UnityEngine.Camera MainCamera;
    [SerializeField] LayerMask aimlayer;

    private float maxDistance = 100f;
    private Vector3 targetPosition;

    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //Debug pro vizualizaci v scene view
        //Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, aimlayer))
        {
            // We hit something! Set the target to the collision point
            targetPosition = hit.point;

        }
        else
        {
            targetPosition = ray.GetPoint(maxDistance);
        }
    }
    void Update()
    {

        // 3. Project the direction onto the plane of the wall/ceiling
        // This prevents the "staring into the ground" tilt while keeping the rotation local
        //Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, surfaceNormal);

        //if (projectedDirection != Vector3.zero)
        //{
            Vector3 direction = targetPosition - transform.position;
            Vector3 surfaceNormal = transform.up;
            ChangeRotation(direction, surfaceNormal);
        //}

    }
    private void ChangeRotation(Vector3 direction, Vector3 surfaceNormal)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, surfaceNormal);
        Vector3 euler = targetRotation.eulerAngles;
        euler.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), Time.deltaTime * 10f);
    }


}
