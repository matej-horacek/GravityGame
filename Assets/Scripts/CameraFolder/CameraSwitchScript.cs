using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitchScript : MonoBehaviour
{
    [SerializeField] CinemachineCamera FirstPersonCam;
    [SerializeField] CinemachineCamera ThirdPersonCam;
    [SerializeField] CinemachineCamera LockedCam;
    [SerializeField] InputActionAsset inputActions;

    private int activeCameraIndex = 0;
    protected FirstPersonAim firstPersonScript;
    protected PlayerAim thirdPersonScript;

    protected InputAction Switch;
    protected List<CinemachineCamera> cameras = new();
    void Awake()
    {
        Debug.Log("Awake call performed ," + activeCameraIndex + "index");
        firstPersonScript = GetComponent<FirstPersonAim>();
        thirdPersonScript = transform.Find("EyePivot").GetComponent<PlayerAim>();
        Switch = inputActions.FindAction("CameraSwitchAction");
        cameras.Add(FirstPersonCam);
        cameras.Add(ThirdPersonCam);
        //cameras.Add(LockedCam);

        cameras[0].Priority = 5;
    }
    private void OnEnable()
    {
        Switch.Enable();
        Switch.performed += Switch_Performed;
    }
    private void OnDisable()
    {
        Switch.Disable();
        Switch.performed -= Switch_Performed;
    }
    protected void Switch_Performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("Switch Performed call, " + activeCameraIndex + "index");
        cameras[activeCameraIndex].Priority = 1;
        activeCameraIndex = (activeCameraIndex + 1) % cameras.Count;
        cameras[activeCameraIndex].Priority = 5;
        switch (activeCameraIndex)
        {
            case 0:
                //Debug.Log("First Person Camera Activated");
                firstPersonScript.enabled = true;
                thirdPersonScript.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case 1:
                //Debug.Log("Third Person Camera Activated");
                firstPersonScript.enabled = false;
                thirdPersonScript.enabled = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
}
