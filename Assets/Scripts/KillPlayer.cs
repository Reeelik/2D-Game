using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Funkcja wywoływana, gdy obiekt wchodzi w kolizję z innym obiektem
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy kolidujący obiekt to gracz
        if (other.CompareTag("Player"))
        {
            // Jeśli tak, wywołujemy funkcję, która powoduje respawn gracza
            LifeController.instance.Respawn();
        }
    }
}