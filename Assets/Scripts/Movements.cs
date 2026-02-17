using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;

//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movements : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Rigidbody rb;
     public float jumpForce = 300f;
    private Animator anim;
    public Vector3 GravityDirection = new Vector3(0, -1, 0);
    protected InputAction Move;
    protected InputAction Jump;
    Vector3 newMove;

    [SerializeField] float speed = 1000;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Move = inputActions.FindAction("Move");
        Jump = inputActions.FindAction("Jump");
    }
    private void OnEnable()
    {
        Move.Enable();
        Jump.Enable();
        Jump.started += Jump_started;
        Jump.performed += Jump_performed;
        Jump.canceled += Jump_canceled;
    }

    private void OnDisable()
    {
        Move.Disable();
        Jump.Disable();
        Jump.performed -= Jump_performed;
    }
    // Update is called once per frame
    void Update()
    {

        newMove = Move.ReadValue<Vector2>();
        newMove.z = newMove.y;
        newMove.y = 0;



    }


    private void FixedUpdate()
    {
        rb.AddRelativeForce((newMove) * speed, ForceMode.Force);
    }


    protected virtual void Jump_started(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump action started");
    }
    protected virtual void Jump_performed(InputAction.CallbackContext obj)
    {
        //anim.SetTrigger("TrJumpStart");
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    protected virtual void Jump_canceled(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump action canceled");
    }



}
