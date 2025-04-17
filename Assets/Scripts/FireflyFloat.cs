using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireflyFloat : MonoBehaviour
{
    private Vector3 startPos;
    private float floatSpeedX;
    private float floatSpeedY;
    private float floatAmplitudeX;
    private float floatAmplitudeY;

    private Light2D fireflyLight;
    private float baseIntensity;
    private float pulseSpeed;
    private float pulseAmplitude;
    private float randomOffset;

    void Start()
    {
        startPos = transform.position;

        // Movimento
        floatSpeedX = Random.Range(0.3f, 0.8f);
        floatSpeedY = Random.Range(0.5f, 1.5f);
        floatAmplitudeX = Random.Range(0.05f, 0.25f);
        floatAmplitudeY = Random.Range(0.15f, 0.45f);
        randomOffset = Random.Range(0f, 100f); // evita sincronização

        // Luz
        fireflyLight = GetComponent<Light2D>();
        if (fireflyLight != null)
        {
            baseIntensity = Random.Range(1.5f, 3f);
            pulseSpeed = Random.Range(1f, 2f);
            pulseAmplitude = baseIntensity * 0.4f;
        }
    }

    void Update()
    {
        // Flutuação 2D (ziguezague)
        float offsetX = Mathf.Cos(Time.time * floatSpeedX + randomOffset) * floatAmplitudeX;
        float offsetY = Mathf.Sin(Time.time * floatSpeedY + randomOffset) * floatAmplitudeY;
        transform.position = new Vector3(startPos.x + offsetX, startPos.y + offsetY, startPos.z);

        // Brilho pulsante
        if (fireflyLight != null)
        {
            fireflyLight.intensity = baseIntensity + Mathf.Sin(Time.time * pulseSpeed + randomOffset) * pulseAmplitude;
        }
    }
}
