using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O personagem
    public float smoothSpeed = 0.125f;

    public float minX; // Lado esquerdo (reta vermelha da esquerda)
    public float maxX; // Lado direito (reta vermelha da direita)

    private float camHalfWidth;

    void Start()
    {
        // Calcula metade da largura da câmera em unidades do mundo
        float camHeight = Camera.main.orthographicSize * 2;
        camHalfWidth = camHeight * Camera.main.aspect / 2;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = transform.position;

        // Siga o personagem no eixo X
        desiredPosition.x = target.position.x;

        // Limite a posição X para não ultrapassar os limites
        float minLimit = minX + camHalfWidth;
        float maxLimit = maxX - camHalfWidth;
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minLimit, maxLimit);

        transform.position = new Vector3(desiredPosition.x, transform.position.y, transform.position.z);
    }
}
