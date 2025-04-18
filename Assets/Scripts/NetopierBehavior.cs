using UnityEngine;

public class NetopierBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform headTransform;
    public SpriteRenderer headRenderer;
    public Sprite eyesOpen;
    public Sprite eyesClosed;
    public float blinkInterval = 2f;
    public float blinkDuration = 0.1f;
    public float detectionRadius = 5f;

    private float blinkTimer;
    private float blinkDurationTimer;
    private bool isBlinking = false;
    private bool eyesAreOpen = true;
    private bool playerIsClose = false;

    void Start()
    {
        blinkTimer = blinkInterval;
    }

    void Update()
    {
        if (eyesAreOpen) { /* lógica futura */ }

        if (player == null) return;

        float distance = Mathf.Abs(player.position.x - transform.position.x);
        bool wasClose = playerIsClose;
        playerIsClose = distance <= detectionRadius;

        if (playerIsClose)
        {
            if (!wasClose)
            {
                // Player acabou de entrar no raio: força abrir os olhos
                OpenEyes();
            }

            LookAtPlayer();
            HandleBlinking();
        }
        else
        {
            CloseEyesAndCenter();
        }

    }

    void LookAtPlayer()
    {
        float direction = player.position.x - transform.position.x;
        float targetZ = direction > 0 ? -10f : 10f;

        Quaternion targetRot = Quaternion.Euler(0, 0, targetZ);
        headTransform.localRotation = Quaternion.Lerp(headTransform.localRotation, targetRot, Time.deltaTime * 5f);
    }

    void HandleBlinking()
    {
        if (!isBlinking)
        {
            blinkTimer -= Time.deltaTime;

            if (blinkTimer <= 0f)
            {
                CloseEyesTemporarily();
            }
        }
        else
        {
            blinkDurationTimer -= Time.deltaTime;

            if (blinkDurationTimer <= 0f)
            {
                OpenEyes();
            }
        }
    }

    void CloseEyesTemporarily()
    {
        headRenderer.sprite = eyesClosed;
        isBlinking = true;
        eyesAreOpen = false;
        blinkDurationTimer = blinkDuration;
    }

    void OpenEyes()
    {
        headRenderer.sprite = eyesOpen;
        isBlinking = false;
        eyesAreOpen = true;
        blinkTimer = blinkInterval;
    }

    void CloseEyesAndCenter()
    {
        headTransform.localRotation = Quaternion.Euler(0, 0, 0);
        headRenderer.sprite = eyesClosed;
        eyesAreOpen = false;
        blinkTimer = blinkInterval;
        isBlinking = false;
    }
}
