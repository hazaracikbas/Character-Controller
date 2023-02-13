using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private float gravity = -9.81f;
    [SerializeField] private float runSpeed = 4;
    [SerializeField] private float sprintSpeed = 6;
    [SerializeField] private float jumpHeight = 4;
    [SerializeField] private float rotationSpeed = 10;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private new Transform camera;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main.transform;
        animator= GetComponent<Animator>();
        speed = runSpeed;
    }

    private void Update()
    {
        PlayerMovement();
        //AnimationParams();

        if (!controller.isGrounded) Debug.Log("Not grounded.");
    }

    private void PlayerMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        direction = Quaternion.Euler(0, camera.eulerAngles.y, 0) * direction;

        animator.SetFloat("vertical", direction.magnitude * speed);

        //Rotates the character to the move direction
        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        }

        velocity.y += gravity * Time.deltaTime;
        velocity.x = direction.x * speed;
        velocity.z = direction.z * speed;

        controller.Move(velocity * Time.deltaTime);

        Jump();
        Sprint();

        //Debug.Log(direction.magnitude * speed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            Debug.Log("Jump!");
            //velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            velocity.y = jumpHeight;
            animator.SetTrigger("Jump");
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && controller.isGrounded)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = runSpeed;

        }
    }
}

