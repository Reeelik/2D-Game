using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private PlayerHealthController healthController; // Kontroler zdrowia gracza

    // Funkcja wywoływana, gdy obiekt wchodzi w kolizję z innym obiektem
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy kolidujący obiekt to gracz
        if (other.CompareTag("Player"))
        {
            // Wywołanie funkcji, która zadaje obrażenia graczowi
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}