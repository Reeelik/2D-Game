using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;

public class Checkpoint : MonoBehaviour  
{  
    private bool isActive; // Czy checkpoint jest aktywny  
    public Animator anim; // Animator dla animacji flagi  

    [HideInInspector]  
    public CheckpointManager cpMan; // Odniesienie do menedżera checkpointów  

    private void OnTriggerEnter2D(Collider2D other)  
    {  
        if (other.tag == "Player" && isActive == false) // Sprawdza, czy gracz wszedł w checkpoint i czy nie jest już aktywny  
        {  
            cpMan.SetActiveCheckPoint(this); // Ustawia ten checkpoint jako aktywny w menedżerze  

            anim.SetBool("flagActive", true); // Aktywuje animację flagi  

            isActive = true; // Ustawia status checkpointu jako aktywny  

            AudioManager.instance.PlayAllSFX(3); // Odtwarza dźwięk aktywacji checkpointu  
        }  
    }  

    public void DeactivateCheckpoint()  
    {  
        anim.SetBool("flagActive", false); // Wyłącza animację flagi  
        isActive = false; // Resetuje status aktywności checkpointu  
    }  
}