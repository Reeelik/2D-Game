using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;

public class BossBattleActivator : MonoBehaviour  
{  
    public BossBattleController theBoss; // Odniesienie do kontrolera walki z bossem

    private void OnTriggerEnter2D(Collider2D other)  
    {  
        if (other.CompareTag("Player")) // Sprawdza, czy obiekt kolidujący ma tag "Player"
        {  
            theBoss.ActiveBattle(); // Aktywuje walkę z bossem
            gameObject.SetActive(false); // Dezaktywuje obiekt aktywatora, aby uniemożliwić ponowne uruchomienie
        }  
    }  
}