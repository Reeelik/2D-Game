using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayerToPlatform : MonoBehaviour
{
    // Funkcja wywoływana, gdy gracz wchodzi w kolizję z platformą
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ustawić gracza jako dziecko platformy (aby podążał za nią)
            other.transform.SetParent(transform);

            // Wyłączenie interpolacji rigidbody, aby obiekt płynnie poruszał się z platformą
            other.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
        }
    }

    // Funkcja wywoływana, gdy gracz wychodzi z kolizji z platformą
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Usunięcie gracza z hierarchii jako dziecko platformy
            other.transform.SetParent(null);

            // Przywrócenie interpolacji rigidbody, aby fizyka obiektu była płynna
            other.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}