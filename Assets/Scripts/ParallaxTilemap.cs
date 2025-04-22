using UnityEngine;
using UnityEngine.Tilemaps;

public class ParallaxTilemap : MonoBehaviour
{
    private Vector3 startPos;
    private float length;
    private Camera cam;

    [Tooltip("0 = move com a câmera, 1 = completamente fixo")]
    public float ParallaxAmountX = 0.5f;
    public float ParallaxAmountY = 0f;
    public bool loop = false;

    private float lastCamX;

    void Start()
    {
        cam = Camera.main;
        startPos = transform.position;
        lastCamX = cam.transform.position.x;

        var tilemapRenderer = GetComponent<TilemapRenderer>();
        length = tilemapRenderer.localBounds.size.x;
    }

    void Update()
    {
        Vector3 camPos = cam.transform.position;
        float deltaX = camPos.x * ParallaxAmountX;
        float deltaY = camPos.y * ParallaxAmountY;

        transform.position = new Vector3(startPos.x + deltaX, startPos.y + deltaY, transform.position.z);

        if (loop)
        {
            float temp = camPos.x * (1 - ParallaxAmountX);
            if (temp > startPos.x + length)
                startPos.x += length;
            else if (temp < startPos.x - length)
                startPos.x -= length;
        }
    }
}
