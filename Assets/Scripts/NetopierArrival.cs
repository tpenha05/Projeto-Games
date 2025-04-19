using UnityEngine;
using System.Collections;

public class NetopierArrival : MonoBehaviour
{
    public GameObject netopierPrefab;
    public Transform startPoint;
    public Transform dropPoint;
    public GameObject player;

    public float flightDuration = 2.5f;
    public float novoMinX = -120.75f; // Novo limite esquerdo da câmera
    public float novoMaxX = 300f;     // (opcional) novo limite direito da câmera

    private Vector3 startScale = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 endScale = Vector3.one;

    void Start()
    {
        StartCoroutine(AnimarChegada());
    }

    IEnumerator AnimarChegada()
    {
        GameObject netopier = Instantiate(netopierPrefab, startPoint.position, Quaternion.identity);
        netopier.transform.localScale = startScale;

        // Aplica fade-in de opacidade nos sprites
        SpriteRenderer[] sprites = netopier.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in sprites)
        {
            Color c = sr.color;
            sr.color = new Color(c.r, c.g, c.b, 0.3f);
        }

        // Oculta o player real durante o voo
        player.SetActive(false);

        // Faz a câmera seguir o Netopier durante o voo
        CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
        if (camFollow != null)
        {
            camFollow.target = netopier.transform;
            camFollow.minX = -9999f; // liberar movimento temporário
        }

        // Voo interpolado com escala e opacidade
        float elapsed = 0f;
        while (elapsed < flightDuration)
        {
            float t = elapsed / flightDuration;

            netopier.transform.position = Vector3.Lerp(startPoint.position, dropPoint.position, t);
            netopier.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            foreach (var sr in sprites)
            {
                Color c = sr.color;
                sr.color = new Color(c.r, c.g, c.b, Mathf.Lerp(0f, 1f, t * 4f));
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Solta o player real no chão
        player.transform.position = dropPoint.position;
        player.SetActive(true);

        // Destroi o netopier visual
        Destroy(netopier);

        // Atualiza a câmera para seguir o player real e os novos limites
        if (camFollow != null)
        {
            camFollow.target = player.transform;
            camFollow.minX = novoMinX;
            camFollow.maxX = novoMaxX;
        }
    }
}
