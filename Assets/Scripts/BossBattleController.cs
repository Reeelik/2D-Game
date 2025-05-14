using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.Serialization;

public class BossBattleController : MonoBehaviour  
{  
    private bool bossActive; // Czy walka z bossem jest aktywna  

    public GameObject blockers; // Obiekt blokujący przejście  

    public Transform camPoint; // Punkt docelowy dla kamery  
    private CameraController camController; // Kontroler kamery  
    public float cameraMoveSpeed; // Prędkość przemieszczania kamery  

    public Transform theBoss; // Transform bossa  

    public float bossGrowSpeed = 2f; // Prędkość powiększania bossa  

    public Transform missileLauncher; // Launcher pocisków  
    public float missileLauncherGrowSpeed = 2f; // Prędkość powiększania launchera  

    public float missileLauncherRotateSpeed = 2f; // Prędkość rotacji launchera  
    private float missileLauncherRotate; // Aktualny kąt rotacji  

    public GameObject projectileToFire; // Pocisk do wystrzelenia  
    public Transform[] projectilePoints; // Punkty startowe pocisków  

    public float waitForShoot; // Czas oczekiwania na rozpoczęcie strzelania  
    public float delayBetweenShots; // Czas między kolejnymi strzałami  

    private float shootStartCounter; // Licznik do rozpoczęcia strzelania  
    private float shotCounter; // Licznik między strzałami  

    private int currentShot; // Aktualny punkt startowy dla strzału  

    public Animator bossAnim; // Animator bossa  

    private bool isWeak; // Czy boss jest podatny na atak  

    public Transform[] bossMovePoints; // Punkty ruchu bossa  
    private int currentMovePoint; // Aktualny punkt ruchu  
    public float bossMoveSpeed; // Prędkość ruchu bossa  

    private int currentPhase; // Aktualna faza walki  

    public GameObject deathEffect; // Efekt śmierci bossa  

    void Start()  
    {  
        camController = FindFirstObjectByType<CameraController>(); // Znajduje kontroler kamery  
        shootStartCounter = waitForShoot; // Ustawia czas początkowy dla strzelania  
        blockers.transform.SetParent(null); // Odłącza blokery od rodzica  
    }  

    void Update()  
    {  
        if (bossActive)  
        {  
            camController.transform.position = Vector3.MoveTowards(camController.transform.position, camPoint.position, cameraMoveSpeed * Time.deltaTime); // Przesuwa kamerę w stronę punktu docelowego  

            if (theBoss.localScale != Vector3.one)  
            {  
               theBoss.localScale = Vector3.MoveTowards(theBoss.localScale, Vector3.one, bossGrowSpeed * Time.deltaTime); // Powiększa bossa  
            }  

            if (missileLauncher.transform.localScale != Vector3.one)  
            {  
                missileLauncher.localScale = Vector3.MoveTowards(missileLauncher.transform.localScale, Vector3.one, bossGrowSpeed * Time.deltaTime); // Powiększa launcher  
            }  

            missileLauncherRotate += missileLauncherRotateSpeed * Time.deltaTime; // Rotacja launchera  
            if (missileLauncherRotate > 360f)  
            {  
                missileLauncherRotate -= 360f;  
            }  
            missileLauncher.transform.localRotation = Quaternion.Euler(0f, 0f, missileLauncherRotate);  

            // Obsługa strzelania  
            if (shootStartCounter > 0f)  
            {  
                shootStartCounter -= Time.deltaTime;  
                if (shootStartCounter <= 0f)  
                {  
                    shotCounter = delayBetweenShots;  
                    FireShot();  
                }  
            }  

            if (shotCounter > 0f)  
            {  
                shotCounter -= Time.deltaTime;  
                if (shotCounter <= 0f)  
                {  
                    shotCounter = delayBetweenShots;  
                    FireShot();  
                }  
            }  

            if (!isWeak)  
            {  
                theBoss.transform.position = Vector3.MoveTowards(theBoss.transform.position, bossMovePoints[currentMovePoint].position, bossMoveSpeed * Time.deltaTime); // Ruch bossa między punktami  
                if (theBoss.transform.position == bossMovePoints[currentMovePoint].position)  
                {  
                    currentMovePoint++;  
                    if (currentMovePoint >= bossMovePoints.Length)  
                    {  
                        currentMovePoint = 0;  
                    }  
                }  
            }  
        }  
    }  

    public void ActiveBattle()  
    {  
        bossActive = true; // Aktywuje walkę  
        blockers.SetActive(true); // Aktywuje blokery  
        camController.enabled = false; // Wyłącza sterowanie kamerą  

        AudioManager.instance.PlayBossMusic(); // Odtwarza muzykę walki z bossem  
    }  

    void FireShot()  
    {  
        Instantiate(projectileToFire, projectilePoints[currentShot].position, projectilePoints[currentShot].rotation); // Wystrzelenie pocisku  

        projectilePoints[currentShot].gameObject.SetActive(false); // Dezaktywuje użyty punkt  
        currentShot++;  
        if (currentShot >= projectilePoints.Length)  
        {  
            shotCounter = 0f;  
            MakeWeak(); // Boss staje się słaby  
        }  

        AudioManager.instance.PlayAllSFX(2); // Dźwięk wystrzału  
    }  

    void MakeWeak()  
    {  
        bossAnim.SetTrigger("isWeak"); // Ustawia animację słabości  
        isWeak = true; // Boss jest podatny na atak  
    }  

    private void OnCollisionEnter2D(Collision2D other)  
    {  
        if (other.gameObject.CompareTag("Player")) // Kolizja z graczem  
        {  
            if (!isWeak)  
            {  
               PlayerHealthController.instance.DamagePlayer(); // Zadaje obrażenia graczowi  
            }  
            else  
            {  
                if (other.transform.position.y > theBoss.position.y)  
                {  
                    bossAnim.SetTrigger("hit"); // Ustawia animację trafienia  
                    FindFirstObjectByType<PlayerController>().Jump(); // Gracz odbija się od bossa  
                    MoveToNextPhase(); // Przechodzi do kolejnej fazy  
                }  
            }  
        }  
    }  

    void MoveToNextPhase()  
    {  
        currentPhase++; // Zwiększa fazę  
        if (currentPhase < 3)  
        {  
            isWeak = false;  
            waitForShoot *= 0.5f; // Skraca czas do strzelania  
            delayBetweenShots *= 0.75f; // Skraca czas między strzałami  
            bossMoveSpeed *= 1.5f; // Przyspiesza ruch bossa  

            shootStartCounter = waitForShoot;  

            missileLauncher.localScale = Vector3.zero; // Resetuje launcher  
            foreach (Transform point in projectilePoints)  
            {  
                point.gameObject.SetActive(true); // Aktywuje wszystkie punkty strzałów  
            }  

            currentShot = 0;  
            AudioManager.instance.PlayAllSFX(1); // Dźwięk przejścia do kolejnej fazy  
        }  
        else  
        {  
            // Zakończenie walki  
            gameObject.SetActive(false); // Dezaktywuje bossa  
            blockers.SetActive(false); // Dezaktywuje blokery  

            camController.enabled = true; // Włącza sterowanie kamerą  

            Instantiate(deathEffect, transform.position, Quaternion.identity); // Efekt śmierci bossa  
            AudioManager.instance.PlayAllSFX(0); // Dźwięk śmierci bossa  
            AudioManager.instance.PlayLvlMusic(FindFirstObjectByType<LvlMusicPlayer>().trackToPlay); // Odtwarza muzykę poziomu  
        }  
    }  
}