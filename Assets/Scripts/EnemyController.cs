using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator anim; // Animator odpowiedzialny za animacje przeciwnika

    [HideInInspector]
    public bool isDefeated; // Flaga określająca, czy przeciwnik został pokonany
    
    public float waitToDestroy; // Czas oczekiwania przed zniszczeniem przeciwnika po pokonaniu
    
    // Start is called before the first frame update
    void Start()
    {
        // Sprawdzamy, czy animator jest przypisany, jeśli nie, próbujemy go pobrać z komponentu obiektu
        if (anim == null)
        {
            anim = GetComponent<Animator>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Jeśli przeciwnik został pokonany, odliczamy czas przed zniszczeniem
        if (isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;
            
            // Jeśli czas do zniszczenia minął, niszczymy przeciwnika
            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);
                
                // Odtwarzamy dźwięk zniszczenia przeciwnika
                AudioManager.instance.PlayAllSFX(5);
            }
        }
    }

    // Funkcja wywoływana, gdy przeciwnik wchodzi w kolizję z innym obiektem
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Jeśli kolidującym obiektem jest gracz
        if (other.gameObject.CompareTag("Player"))
        {
            // Jeśli przeciwnik nie jest jeszcze pokonany, zadajemy obrażenia graczowi
            if (isDefeated == false)
            {
                PlayerHealthController.instance.DamagePlayer();
            }
        }
    }

    // Funkcja wywoływana, gdy przeciwnik wchodzi w obszar triggera
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jeśli kolidującym obiektem jest gracz
        if (other.gameObject.CompareTag("Player"))
        {
            // Sprawiamy, że gracz skacze
            FindFirstObjectByType<PlayerController>().Jump();
            
            // Ustawiamy animację pokonania przeciwnika
            anim.SetTrigger("defeated");
            isDefeated = true; // Ustawiamy flagę pokonania przeciwnika
            // Odtwarzamy dźwięk pokonania przeciwnika
            AudioManager.instance.PlayAllSFX(6);
        }
    }
}
