using UnityEngine;
using System.Collections;

public class NetopierArrival : MonoBehaviour
{
    public GameObject netopierPrefab;
    public Transform startPoint;
    public Transform dropPoint;
    public GameObject player;

    public float flightDuration = 2.5f;
    public float novoMinX = -127.25f;
    public float novoMaxX = 300f;

    private Vector3 startScale = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 endScale = Vector3.one;

    public float zoomInicio = 2.8f;
    public float zoomDuranteVoo = 4.2f;
    public float zoomFinalPlayer = 5.0f;
    public float tempoZoomNoPlayer = 1.5f;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return null;
        StartCoroutine(AnimarChegada());
    }

    IEnumerator AnimarChegada()
    {
        GameObject netopier = Instantiate(netopierPrefab, startPoint.position, Quaternion.identity);
        netopier.transform.localScale = startScale;

        // Acessa a parte do morcego que deve ser movimentada
        Transform mover = netopier.transform.Find("Netopier/Torso");

        player.SetActive(false);

        CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
        if (camFollow != null)
        {
            camFollow.target = netopier.transform;
            camFollow.minX = -9999f;
        }

        Camera.main.orthographicSize = zoomInicio;
        yield return StartCoroutine(ZoomCamera(zoomDuranteVoo, flightDuration));

        float elapsed = 0f;
        while (elapsed < flightDuration)
        {
            float t = elapsed / flightDuration;

            if (mover != null)
                mover.position = Vector3.Lerp(startPoint.position, dropPoint.position, t);

            netopier.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        player.transform.position = dropPoint.position;
        player.SetActive(true);

        Destroy(netopier);

        if (camFollow != null)
        {
            camFollow.target = player.transform;

            yield return StartCoroutine(ZoomCamera(zoomFinalPlayer, 0.7f));
            yield return new WaitForSeconds(tempoZoomNoPlayer);
            yield return StartCoroutine(ZoomCamera(5.5f, 1.5f));

            camFollow.minX = novoMinX;
            camFollow.maxX = novoMaxX;
        }
    }

    IEnumerator ZoomCamera(float targetSize, float duration)
    {
        float startSize = Camera.main.orthographicSize;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.orthographicSize = targetSize;
    }
}
