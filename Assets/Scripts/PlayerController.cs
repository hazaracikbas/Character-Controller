using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private float gravity = -9.81f;
    public float Gravity
    {
        get { return gravity; }
        set { gravity = value; }
    }

    [SerializeField] private float jumpHeight;
    public float JumpHeight
    {
        get { return jumpHeight; }
        set { jumpHeight = value; }
    }

    [SerializeField] private float rotationSpeed;
    public float RotationSpeed
    {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }

    [SerializeField] private float runSpeed;
    public float RunSpeed
    {
        get { return runSpeed; }
        set { runSpeed = value; }
    }

    [SerializeField] private float sprintSpeed;
    public float SprintSpeed
    {
        get { return sprintSpeed; }
        set { sprintSpeed = value; }
    }
    #endregion

    [HideInInspector] public CharacterController controller;
    [HideInInspector] public PlayerInput input;
    [HideInInspector] public Animator animator;
    [HideInInspector] public new Transform camera;
    [HideInInspector] public Vector3 velocity;

    private Vector2 moveInput;
    private InputAction movement;
    private InputAction jump;
    private InputAction sprint;
    private InputAction dodge;

    private StateMachine stateMachine;
    private IdleState idle;
    private RunState running;
    private JumpState jumping;

    private bool isJumping;
    private bool isMoving;
    private bool isFalling;
    private bool isDodging;

    private void Awake()
    {
        camera = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        input = new PlayerInput();

        movement = input.Player.Movement;
        jump = input.Player.Jump;
        sprint = input.Player.Sprint;
        dodge = input.Player.Dodge;

    }
    private void Start()
    {
        speed = runSpeed;

        stateMachine = new StateMachine();
        idle = new IdleState(this, stateMachine);
        running = new RunState(this, stateMachine);
        jumping = new JumpState(this, stateMachine);

        stateMachine.Initialize(idle);

    }
    private void Update()
    {
        Movement();

        if (controller.isGrounded) { isJumping = false; }
    }

    private void OnEnable()
    {
        movement.performed += OnMovementPerformed;
        movement.canceled += OnMovementCanceled;
        jump.performed += OnJump;
        sprint.performed += OnSprintPerformed;
        sprint.canceled += OnSprintCanceled;

        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Movement()
    {
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        
        direction = Quaternion.Euler(0, camera.eulerAngles.y, 0) * direction;

        // Sets camera forward as forward direction
        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        }

        velocity.x = direction.x * speed;
        velocity.z = direction.z * speed;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        animator.SetFloat("vertical", direction.magnitude * speed);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            velocity.y = jumpHeight;
            animator.SetTrigger("Jump");
            isJumping = true;
        }
    }
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        speed = sprintSpeed;
    }
    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        speed = runSpeed;
    }
}

