using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public static LifeController instance;

    private void Awake()
    {
        instance = this;
    }

    private PlayerController thePlayer;
    
    public float respawnDelay = 2f;
    public int currentLifes = 3;
    
    public GameObject deathEffect;
    public GameObject respawnEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindFirstObjectByType<PlayerController>();
        currentLifes = InfoTracker.instance.currentLives;
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        thePlayer.gameObject.SetActive(false);
        thePlayer.theRB.velocity = Vector2.zero;
        currentLifes--;
        if (currentLifes > 0)
        {
            StartCoroutine(RespawnCo());
        }
        else
        {
            currentLifes = 0;
            StartCoroutine(GameOverCo());
        }

        UpdateDisplay();
        Instantiate(deathEffect, thePlayer.transform.position, deathEffect.transform.rotation);
        
        AudioManager.instance.PlayAllSFX(11);


    }

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(respawnDelay);
        
        thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;
        PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);
        
        thePlayer.gameObject.SetActive(true);
        
        Instantiate(respawnEffect, thePlayer.transform.position, Quaternion.identity);
    }

    public IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(respawnDelay);
        if (UlController.instance != null)
        {
            UlController.instance.ShowGameOver();
        }
    }

    public void AddLife()
    {
        currentLifes++;
        
        UpdateDisplay();
        
        AudioManager.instance.PlayAllSFX(8);

    }

    public void UpdateDisplay()
    {
        if (UlController.instance != null)
        {
            UlController.instance.UpdateLivesDisplay(currentLifes);
        }
    }
}
