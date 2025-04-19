using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    public float minX;
    public float maxX;

    private float camHalfWidth;

    void Start()
    {
        float camHeight = Camera.main.orthographicSize * 2;
        camHalfWidth = camHeight * Camera.main.aspect / 2;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = transform.position;
        desiredPosition.x = target.position.x;

        float minLimit = minX + camHalfWidth;
        float maxLimit = maxX - camHalfWidth;
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minLimit, maxLimit);

        transform.position = new Vector3(desiredPosition.x, transform.position.y, transform.position.z);
    }
}
