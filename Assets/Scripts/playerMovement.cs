using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;

    //Player info
    public float speed = 8.0f;
    public float gravity = 20f;
    readonly float jumpHeight = 1.5f;

    //For ground check
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public bool isGrounded;

    public Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(2.0f * gravity * jumpHeight);
            print("Jump");
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x * 0.4f + transform.forward * z; // I put to strafe left and right slower :)
        velocity.y -=  gravity * Time.deltaTime;

        controller.Move(Time.deltaTime * speed * move);
        controller.Move(Time.deltaTime * velocity);
    }
}
