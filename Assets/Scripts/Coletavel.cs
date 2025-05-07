using UnityEngine;

public class Coletavel : MonoBehaviour
{
    // Opcional: som ou efeito visual pode ser adicionado aqui

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aqui você pode adicionar lógica como pontuação, inventário, etc.
            Debug.Log("Item coletado!");

            // Destrói o item da cena
            Destroy(gameObject);
        }
    }
}
