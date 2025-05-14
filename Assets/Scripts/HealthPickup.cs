using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToAdd; // Ilość zdrowia do dodania
    public GameObject pickupEffect; // Efekt wizualny po podniesieniu przedmiotu
    public bool giveFullHealth; // Flaga, czy podnieść pełne zdrowie

    // Funkcja wywoływana, gdy obiekt wchodzi w kolizję z innym obiektem
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy kolidujący obiekt to gracz
        if (other.CompareTag("Player"))
        {
            // Jeśli gracz nie ma pełnego zdrowia, dodaj zdrowie
            if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
            {
                // Jeśli flaga wskazuje na pełne zdrowie, przywracamy całe zdrowie
                if (giveFullHealth == true)
                {
                    PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);
                }
                else
                {
                    // W przeciwnym przypadku dodajemy określoną ilość zdrowia
                    PlayerHealthController.instance.AddHealth(healthToAdd);
                }

                // Zniszczenie obiektu po podniesieniu
                Destroy(gameObject);
                // Zainicjowanie efektu wizualnego w miejscu podniesienia
                Instantiate(pickupEffect, transform.position, transform.rotation);
                
                // Odtwarzanie dźwięku podniesienia przedmiotu
                AudioManager.instance.PlayAllSFX(10);
            }
        }
    }
}