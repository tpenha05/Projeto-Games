using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ========== Vida ==========
    public int maxHealth = 5;
    private int currentHealth;

    // ========== Movimento ==========
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    // ========== Referências ==========
    private Rigidbody2D rb;
    private Animator animator;
    private float moveX;
    private SpriteRenderer spriteRenderer;

    // ========== Verificação de Chão ==========
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;



    // ========== Ataque Melee ==========
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        // Animação de andar
        animator.SetBool("isWalking", moveX != 0);

        // Verifica se está no chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Verifica se está caindo
        bool isFalling = rb.linearVelocity.y < -0.1f && !isGrounded;
        animator.SetBool("isFalling", isFalling);

        // Verifica se está pulando
        bool isJumping = rb.linearVelocity.y > 0.1f && !isGrounded;
        animator.SetBool("isJumping", isJumping);

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Flip do sprite
        if (moveX > 0) spriteRenderer.flipX = false;
        else if (moveX < 0) spriteRenderer.flipX = true;

        // Ataque
        if (Input.GetMouseButtonDown(0) )
        {
            animator.SetTrigger("Attack");
            
        }
    }
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Vida atual: " + currentHealth);
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveX * moveSpeed;
        rb.linearVelocity = velocity;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Inimigo atingido: " + enemy.name);
            enemy.GetComponent<Enemy>()?.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
