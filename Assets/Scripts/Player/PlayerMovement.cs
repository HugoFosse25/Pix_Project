using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float groundCheckRadius;

    public Transform groundCheck;
    public LayerMask collisionLayers;

    private bool isGrounded;
    private bool isJumpButtonDown;

    private float horizontalMovement;
    float characterVelocity;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump"))
        {
            isJumpButtonDown = true;
        }
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        
        MovePlayer(horizontalMovement * moveSpeed * Time.deltaTime);

        characterVelocity = Mathf.Abs(rb.velocity.x); //Keep the velocity positive
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("IsGrounded", isGrounded);

        Flip(rb.velocity.x);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref targetVelocity, .05f); //.05f == SmoothTime

        if (isJumpButtonDown && isGrounded)   //Player push jumping Button
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumpButtonDown = false;
            
        }
    }

        void Flip(float _velocity)
    {
         if(_velocity > 0.05f)
        {
            spriteRenderer.flipX = false;
        }
        else if(_velocity < -0.05f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos() //Drawn circle for groundCheck
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
