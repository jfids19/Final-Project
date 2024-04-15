using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float jumpForce;
    private float originalJumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Animation")]
    public Animator animator;
    public Animator spiritAnimator;


    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    private CapsuleCollider capsuleCollider;
    private Vector3 originalColliderCenter;
    private float originalColliderHeight;
    private float originalColliderRadius;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode spiritKey = KeyCode.E;
    public KeyCode formKey = KeyCode.Q;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    public Transform player;
    public Transform playerObject;
    public Transform spiritPlayerObject;
    public float yOffset;

    private bool isHumanActive = true;
    private bool isSpiritActive = false;

    [Header("Audio")]
    public AudioSource footstepAudioSource;
    public AudioSource formChange;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

        startYScale = transform.localScale.y;

        capsuleCollider = GetComponent<CapsuleCollider>();
        originalColliderCenter = capsuleCollider.center;
        originalColliderHeight = capsuleCollider.height;
        originalColliderRadius = capsuleCollider.radius;

        //scale spirit player
        spiritPlayerObject.localScale = new Vector3(0.3f,0.3f,0.3f);

        //activation of players
        playerObject.gameObject.SetActive(true);
        spiritPlayerObject.gameObject.SetActive(false);

        originalJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {  
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        //visualise the raycast
        Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.red);
        
        MyInput();
        SpeedControl();
        StateHandler();

        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        UpdateAnimator();
        audioController();
    }

    private void FixedUpdate()
    {
        MovePlayer();

        //sync
        playerObject.position = transform.position + Vector3.up * yOffset;
        spiritPlayerObject.position = transform.position + Vector3.up * yOffset;

        //activation of players
        if (isHumanActive)
        {
            playerObject.gameObject.SetActive(true);
            spiritPlayerObject.gameObject.SetActive(false);
        }
        else if (isSpiritActive)
        {
            playerObject.gameObject.SetActive(false);
            spiritPlayerObject.gameObject.SetActive(true);
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
           readyToJump = false;

           Jump();

           Invoke(nameof(ResetJump), jumpCooldown); 
        }

        //start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            if(isHumanActive == true)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale,transform.localScale.z);
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
                moveSpeed = crouchSpeed;
                capsuleCollider.height = 4.35f;
                capsuleCollider.center = new Vector3(0f, -0.93f, 0f);
            }
            else if(isSpiritActive == true)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale,transform.localScale.z);
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
                moveSpeed = crouchSpeed;
                capsuleCollider.height = 1.38f;
                capsuleCollider.radius = 0.35f;
                capsuleCollider.center = new Vector3(0f, -2.36f, 0f);
            }
        }

        //stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            if(isHumanActive == true)
            {
                transform.localScale = new Vector3(transform.localScale.x, startYScale,transform.localScale.z);
                capsuleCollider.height = originalColliderHeight;
                capsuleCollider.center = originalColliderCenter;
            }
            else if(isSpiritActive == true)
            {
                transform.localScale = new Vector3(transform.localScale.x, startYScale,transform.localScale.z);
                capsuleCollider.height = 1.5f;
                capsuleCollider.radius = 0.35f;
                capsuleCollider.center = new Vector3(0f,-1.96f,0f);
            }
        }

        if(Input.GetKeyDown(formKey))
        {
            if(isHumanActive == true)
            {
                isHumanActive = false;
                isSpiritActive = true;
                walkSpeed = 4;
                sprintSpeed = 5;
                capsuleCollider.height = 1.5f;
                capsuleCollider.radius = 0.35f;
                capsuleCollider.center = new Vector3(0f,-1.96f,0f);
                formChange.Play();
            }
            else if(isSpiritActive == true)
            {
                isSpiritActive = false;
                isHumanActive = true;
                walkSpeed = 7;
                sprintSpeed = 9;
                capsuleCollider.height = originalColliderHeight;
                capsuleCollider.radius = originalColliderRadius;
                capsuleCollider.center = originalColliderCenter;
                formChange.Play();
            }
        }
    }

    private void StateHandler()
    {        
        //mode - crouching
        if(Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        
        //mode - sprinting
        if(grounded && Input.GetKey(sprintKey) &! Input.GetKey(crouchKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
            animator.SetBool("IsRunning", true);
            spiritAnimator.SetBool("IsRunning", true);

        }

        //mode - walking
        else if(grounded &! Input.GetKey(crouchKey))
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            animator.SetBool("IsRunning", false);
            spiritAnimator.SetBool("IsRunning", false);
        }

        //mode - air
        else
        {
            state = MovementState.air;
        }

        //Debug.Log(state);
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
        }   
        //on ground
        else if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //in air 
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        //turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        //limiting speed on slope
        if(OnSlope() && !exitingSlope)
        {
            if(rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

         //limit velocity if needed
            if(flatVel.magnitude > moveSpeed)
            {
                 Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
             }
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        Debug.Log("Jumping!");
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private void UpdateAnimator()
    {
        bool isMoving = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;
        animator.SetBool("IsMoving", isMoving);
        spiritAnimator.SetBool("IsMoving", isMoving);

        if(Input.GetKeyDown(crouchKey))
        {
            animator.SetBool("IsCrouching", true);
            spiritAnimator.SetBool("IsCrouching", true);
        }
        else if(Input.GetKeyUp(crouchKey))
        {
            animator.SetBool("IsCrouching", false);
            spiritAnimator.SetBool("IsCrouching", false);
        }

        if(Input.GetKeyDown(jumpKey))
        {
            animator.SetTrigger("JumpTrigger");
            spiritAnimator.SetTrigger("JumpTrigger");
        }

        if(grounded)
        {
            animator.SetBool("IsGrounded", true);
            spiritAnimator.SetBool("IsGrounded", true);
        }
        else if(!grounded)
        {
            animator.SetBool("IsGrounded", false);
            spiritAnimator.SetBool("IsGrounded", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
    }

    void audioController()
    {
        bool isMoving = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;
        
        if(grounded)
        {
            if(isMoving)
            {   
                if(!footstepAudioSource.isPlaying)
                {
                    footstepAudioSource.Play();
                }
            }
            else
            {
                if(footstepAudioSource.isPlaying)
                {
                    footstepAudioSource.Stop();
                }
            }
        }
        else
        {
            if(footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }
        }

        if(state == MovementState.sprinting)
        {
            footstepAudioSource.pitch = 1.3f;
        }
        else
        {
            footstepAudioSource.pitch = 1.0f;
        }
    }

    public void AdjustJumpForce(float multiplier)
    {
        jumpForce *= multiplier;
    }

    public void ResetJumpForce()
    {
        jumpForce = originalJumpForce;
    }
}
