using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 5f;

    void Update()
    {
        transform.position += Vector3.left * velocidade * Time.deltaTime;
    }
}
