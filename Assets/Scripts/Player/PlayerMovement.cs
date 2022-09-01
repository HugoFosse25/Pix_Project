using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float groundCheckRadius;

    public Transform groundCheck;
    public LayerMask collisionLayers;

    private bool isGrounded;
    private bool isJumpButtonDown;
    private bool canDash = true;
    private bool isDashing;

    private int playerDirection; // if value = 1 == flipX=false ; -1 = flipX=true

    private float horizontalMovement;
    private float characterVelocity;
    private float dashingPower = 18f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private TrailRenderer tr;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }

    private void Update()
    {

        if (isDashing)  //The player cannot move while isDashing
        {
            return;
        }

        horizontalMovement = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump"))
        {
            isJumpButtonDown = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        if (isDashing)//The player cannot move while isDashing
        {
            return;
        }

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
            playerDirection = 1;
        }
        else if(_velocity < -0.05f)
        {
            spriteRenderer.flipX = true;
            playerDirection = -1;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;   //Remove gravity
        //Start dashing
        rb.velocity = new Vector2(playerDirection * dashingPower, 0f);
        tr.emitting = true; //Enabled the dash effect
        yield return new WaitForSeconds(dashingTime);
        //Stop Dashing
        tr.emitting = false;
        rb.gravityScale = originalGravity;  //Set original gravity
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnDrawGizmos() //Drawn circle for groundCheck
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
