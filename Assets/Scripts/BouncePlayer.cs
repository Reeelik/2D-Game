using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;

public class BouncePlayer : MonoBehaviour  
{  
    public float bounceForce; // Siła, z jaką gracz zostanie odbity  

    public Animator anim; // Animator do obsługi animacji odbicia  

    private void OnTriggerEnter2D(Collider2D other)  
    {  
        if (other.CompareTag("Player")) // Sprawdza, czy obiekt kolidujący to gracz  
        {  
            anim.SetTrigger("Bounce"); // Uruchamia animację odbicia  

            other.GetComponent<PlayerController>().BouncePlayer(bounceForce); // Wywołuje metodę odbicia gracza z określoną siłą  
        }  
    }  
}