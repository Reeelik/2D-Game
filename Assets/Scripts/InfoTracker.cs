using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTracker : MonoBehaviour
{
    public static InfoTracker instance; // Statyczna instancja InfoTracker, dostępna globalnie
    public int currentLives; // Liczba żyć gracza
    public int currentFruit; // Liczba zebranych owoców (lub innych przedmiotów)

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Sprawdzenie, czy instancja już istnieje, aby uniknąć duplikatów
        if (instance == null)
        {
            instance = this; // Ustawienie instancji na ten obiekt
            transform.SetParent(null); // Usunięcie obiektu z hierarchii w celu zapewnienia, że będzie niezależny
            DontDestroyOnLoad(gameObject); // Zapewnienie, że obiekt nie zostanie zniszczony przy ładowaniu nowej sceny

            // Załadowanie danych zapisanych w PlayerPrefs, jeśli istnieją
            if (PlayerPrefs.HasKey("lives"))
            {
                currentLives = PlayerPrefs.GetInt("lives");
                currentFruit = PlayerPrefs.GetInt("fruit");
            }
        }
        else
        {
            Destroy(gameObject); // Zniszczenie duplikatu obiektu, jeśli już istnieje instancja
        }
    }

    // Funkcja do pobrania informacji o życiu i owocach z innych menedżerów
    public void GetInfo()
    {
        // Jeśli istnieje instancja LifeController, pobieramy aktualną liczbę żyć
        if (LifeController.instance != null)
        {
            currentLives = LifeController.instance.currentLifes;
        }

        // Jeśli istnieje instancja CollectiblesManager, pobieramy aktualną liczbę owoców
        if (CollectiblesManager.instance != null)
        {
            currentFruit = CollectiblesManager.instance.collectibleCount;
        }
    }

    // Funkcja zapisująca dane do PlayerPrefs
    public void SaveInfo()
    {
        // Zapisanie liczby żyć i owoców w PlayerPrefs
        PlayerPrefs.SetInt("lives", currentLives);
        PlayerPrefs.SetInt("fruit", currentFruit);
    }
}