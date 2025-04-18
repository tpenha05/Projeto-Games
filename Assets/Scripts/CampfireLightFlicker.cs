using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CampfireLight2DFlicker : MonoBehaviour
{
    [Header("Intensidade da Luz")]
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;

    [Header("Velocidade da Variação")]
    public float flickerSpeed = 5f;

    private Light2D fireLight;
    private float targetIntensity;

    void Start()
    {
        fireLight = GetComponent<Light2D>();
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    void Update()
    {
        // Aproxima a intensidade atual da intensidade alvo
        fireLight.intensity = Mathf.Lerp(fireLight.intensity, targetIntensity, Time.deltaTime * flickerSpeed);

        // Se estiver próximo da intensidade alvo, sorteia uma nova
        if (Mathf.Abs(fireLight.intensity - targetIntensity) < 0.02f)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
