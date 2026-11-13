using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movedirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        speedControl();

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            jump();

            Invoke(nameof(Resetjump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        movedirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(movedirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(movedirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void speedControl()
    {
        Vector3 flatvel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);


        if (flatvel.magnitude > moveSpeed)
        {
            Vector3 limitedvel = flatvel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedvel.x, rb.linearVelocity.y, limitedvel.z);
        }
    }

    private void jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void Resetjump()
    {
        readyToJump = true;
    }

}