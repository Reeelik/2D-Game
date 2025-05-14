using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;

public class BossProjectile : MonoBehaviour  
{  
    public float speed = 8f; // Prędkość pocisku  
    private Vector3 direction; // Kierunek ruchu pocisku  
    public float lifeTime = 3f; // Czas życia pocisku  

    // Wywoływane na początku działania obiektu  
    void Start()  
    {  
        direction = (PlayerHealthController.instance.transform.position - transform.position).normalized; // Ustawia kierunek w stronę gracza  
        Destroy(gameObject, lifeTime); // Automatyczne zniszczenie pocisku po określonym czasie  
    }  

    // Wywoływane co klatkę  
    void Update()  
    {  
        transform.position += direction * (Time.deltaTime * speed); // Przesuwa pocisk w określonym kierunku  
    }  

    private void OnTriggerEnter2D(Collider2D other)  
    {  
        if (other.CompareTag("Player")) // Sprawdza, czy pocisk trafił gracza  
        {  
            PlayerHealthController.instance.DamagePlayer(); // Zadaje obrażenia graczowi  
            Destroy(gameObject); // Niszczy pocisk po trafieniu  
        }  
    }  
}