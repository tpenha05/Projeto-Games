using UnityEngine;

public class ColunaArmadilha : MonoBehaviour
{
    public GameObject aviso;
    public float tempoAntesDoAviso = 0.2f; // Pequeno delay antes do aviso aparecer (opcional)
    public float duracaoAviso = 3f;        // Quanto tempo o aviso fica visível
    public float forcaQueda = 10f;
    public GameObject player;

    private Rigidbody2D rb;
    private bool ativou = false;
    private bool caiu = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        if (aviso != null)
        {
            aviso.SetActive(false);
        }
    }

    void Update()
    {
        if (!ativou && PlayerAbaixo())
        {
            ativou = true; // só ativa uma vez
            Invoke(nameof(AtivarAviso), tempoAntesDoAviso);
        }
    }

    bool PlayerAbaixo()
    {
        float colLeft = transform.position.x - transform.localScale.x / 2f;
        float colRight = transform.position.x + transform.localScale.x / 2f;
        float playerX = player.transform.position.x;

        return playerX >= colLeft && playerX <= colRight;
    }

    void AtivarAviso()
    {
        if (aviso != null)
        {
            aviso.SetActive(true);
        }

        Invoke(nameof(Derrubar), duracaoAviso);
    }

    void Derrubar()
    {
        if (aviso != null)
        {
            aviso.SetActive(false);
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.down * forcaQueda;
        caiu = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (caiu && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
