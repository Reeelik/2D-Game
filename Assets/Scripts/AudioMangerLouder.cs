using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;

public class AudioMangerLouder : MonoBehaviour  
{  
    public AudioManager theAM; // Odniesienie do prefabrykatowej instancji AudioManagera

    private void Awake()  
    {  
        if (AudioManager.instance == null) // Sprawdza, czy instancja AudioManagera istnieje
        {  
            Instantiate(theAM).SetupAudioManager(); // Tworzy nową instancję AudioManagera i ustawia ją
        }  
    }  
}