using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    public int amount = 1; // Ilość zbieranych przedmiotów
    public GameObject pickupEffect; // Efekt wizualny po podniesieniu przedmiotu

    // Funkcja wywoływana, gdy obiekt wchodzi w kolizję z innym obiektem
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy kolidujący obiekt to gracz
        if (other.CompareTag("Player"))
        {
            // Sprawdzamy, czy instancja CollectiblesManager istnieje
            if (CollectiblesManager.instance != null)
            {
                // Dodanie przedmiotów do kolekcji gracza
                CollectiblesManager.instance.GetCollectibles(amount);
                
                // Zniszczenie obiektu po podniesieniu
                Destroy(gameObject);

                // Zainicjowanie efektu wizualnego w miejscu podniesienia
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
                
                // Odtworzenie dźwięku podniesienia przedmiotu
                AudioManager.instance.PlaySFXPitched(9);
            }
        }
    }
}