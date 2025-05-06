using UnityEngine;

public class FlorAtiradora : MonoBehaviour
{
    public GameObject projetilPrefab;  // Prefab do que a flor vai disparar
    private Animator animator; // Referência ao Animator da flor
    public Transform pontoDisparo;     // Onde o projetil será instanciado
    public float intervaloDisparo = 3f;

    private float tempoUltimoDisparo = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        tempoUltimoDisparo += Time.deltaTime;

        if (tempoUltimoDisparo >= intervaloDisparo)
        {
            Atirar();
            tempoUltimoDisparo = 0f;
        }
    }

    void Atirar()
    {
        animator.SetTrigger("Atirar"); // Ativa a animação de disparo


        Instantiate(projetilPrefab, pontoDisparo.position, pontoDisparo.rotation);
    }
}
