using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private bool isGrounded;

    private float moveX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Pega input do teclado (A/D ou Setas)
        moveX = Input.GetAxisRaw("Horizontal");

        // Animação
        bool isWalking = moveX != 0;
        animator.SetBool("isWalking", isWalking);

        // Flip do sprite se mudar de direção
        if (moveX > 0) spriteRenderer.flipX = false;
        else if (moveX < 0) spriteRenderer.flipX = true;
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump!");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveX * moveSpeed;
        rb.linearVelocity = velocity;
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Jump!");
        }


    }
}
