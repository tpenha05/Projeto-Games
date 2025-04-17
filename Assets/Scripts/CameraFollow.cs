using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // O alvo a seguir (o Player)
    public float smoothSpeed = 0.125f; // Suavização do movimento
    public Vector3 offset;        // Offset para ajustar a posição da câmera em relação ao player

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z); // Mantém o Z fixo
    }
}
    