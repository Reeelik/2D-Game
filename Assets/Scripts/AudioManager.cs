using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour  
{  
    public static AudioManager instance; // Singleton AudioManager, zapewnia dostęp do jednej instancji

    private void Awake()  
    {  
        if (instance == null)  
        {  
            SetupAudioManager(); // Inicjalizacja instancji i konfiguracja AudioManagera
        }  
        else if (instance != this)  
        {  
            Destroy(gameObject); // Usuwa duplikaty obiektów AudioManager
        }  
    }  

    public void SetupAudioManager()  
    {  
        instance = this; // Ustawienie bieżącej instancji
        DontDestroyOnLoad(gameObject); // Zapewnienie, że AudioManager nie zostanie zniszczony między scenami
    }  

    public AudioSource menuMusic; // Źródło audio dla muzyki w menu
    public AudioSource bossMusic; // Źródło audio dla muzyki podczas walki z bossem
    public AudioSource lvlComplateMusic; // Źródło audio dla muzyki po ukończeniu poziomu
    public AudioSource[] lvlMusic; // Tablica źródeł audio dla muzyki poziomów

    public AudioSource[] allSFX; // Tablica źródeł audio dla efektów dźwiękowych

    void StopMusic()  
    {  
        menuMusic.Stop(); // Zatrzymuje muzykę menu
        bossMusic.Stop(); // Zatrzymuje muzykę bossa
        lvlComplateMusic.Stop(); // Zatrzymuje muzykę ukończenia poziomu

        foreach (AudioSource track in lvlMusic)  
        {  
            track.Stop(); // Zatrzymuje wszystkie ścieżki muzyczne poziomów
        }  
    }  

    public void PlayMenuMusic()  
    {  
        StopMusic(); // Zatrzymuje wszystkie ścieżki przed odtworzeniem nowej
        menuMusic.Play(); // Odtwarza muzykę menu
    }  

    public void PlayBossMusic()  
    {  
        StopMusic();  
        bossMusic.Play(); // Odtwarza muzykę bossa
    }  

    public void PlayLvlComplateMusic()  
    {  
        StopMusic();  
        lvlComplateMusic.Play(); // Odtwarza muzykę ukończenia poziomu
    }  

    public void PlayLvlMusic(int trackToPlay)  
    {  
        StopMusic();  
        lvlMusic[trackToPlay].Play(); // Odtwarza wybraną ścieżkę muzyki poziomu
    }  

    public void PlayAllSFX(int sfxToPlay)  
    {  
        allSFX[sfxToPlay].Stop(); // Zatrzymuje efekt dźwiękowy, jeśli już gra
        allSFX[sfxToPlay].Play(); // Odtwarza wybrany efekt dźwiękowy
    }  

    public void PlaySFXPitched(int sfxToPlay)  
    {  
        allSFX[sfxToPlay].Stop();  
        allSFX[sfxToPlay].pitch = Random.Range(.75f, 1.25f); // Ustawia losowe nachylenie dźwięku
        allSFX[sfxToPlay].Play(); // Odtwarza efekt dźwiękowy z nowym nachyleniem
    }  
}