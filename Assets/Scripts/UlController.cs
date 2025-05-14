using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UlController : MonoBehaviour
{
    public static UlController instance;

    // Inicjalizacja instancji, żeby było łatwiej sięgnąć do tego obiektu z innych skryptów
    private void Awake()
    {
        instance = this;
    }

    public Image[] heartIcons;  // Tablica ikon serc, która pokazuje zdrowie gracza
    public Sprite heartFull;  // Pełne serce
    public Sprite heartEmpty; // Puste serce
    public TMP_Text livesText;  // Wyświetlanie liczby żyć
    public TMP_Text CollectiblesText;  // Wyświetlanie liczby zebranych przedmiotów (owoców)
    public GameObject gameOverScreen;  // Ekran game over
    public GameObject pauseScreen;  // Ekran pauzy
    public string mainMenuScene;  // Nazwa sceny głównego menu
    public Image fadeScreen;  // Ekran przejścia (fade)
    public float fadeTime;  // Czas trwania efektu fade

    public bool fadingToBlack;  // Flaga do kontroli przejścia do czerni
    public bool fadingFromBlack;  // Flaga do kontroli przejścia z czerni

    // Start jest wywoływane przed pierwszą klatką
    void Start()
    {
        FadeFromBlack();  // Zaczynamy od efektu fade z czerni
    }

    // Update jest wywoływane raz na klatkę
    void Update()
    {
        // Sprawdzenie, czy naciśnięto Escape - jeśli tak, włączamy pauzę
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        // Obsługuje efekt przejścia do czerni (fade out)
        if (fadingFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeTime * Time.deltaTime));
        }

        // Obsługuje efekt przejścia z czerni (fade in)
        if (fadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeTime * Time.deltaTime));
        }
    }

    // Aktualizowanie wyświetlania zdrowia na podstawie liczby punktów zdrowia
    public void UpdateHealthDisplay(int health, int maxHealth)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;  // Włączamy wszystkie ikony serc

            if (health > i)
            {
                heartIcons[i].sprite = heartFull;  // Ustawiamy pełne serce, jeśli gracz ma wystarczająco dużo zdrowia
            }
            else
            {
                heartIcons[i].sprite = heartEmpty;  // Ustawiamy puste serce, jeśli gracz ma mniej zdrowia niż index ikony

                if (maxHealth <= i)
                {
                    heartIcons[i].enabled = false;  // Ukrywamy ikony serc, które nie są potrzebne (np. jeśli maxHealth to 3, a mamy 5 serc w tablicy)
                }
            }
        }
    }

    // Aktualizowanie liczby żyć na wyświetlaczu
    public void UpdateLivesDisplay(int currentLives)
    {
        livesText.text = currentLives.ToString();  // Wyświetlamy liczbę żyć
    }

    // Pokazanie ekranu Game Over
    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);  // Ustawiamy ekran game over jako aktywny
    }

    // Restartowanie gry (ponowne załadowanie aktualnej sceny)
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Ładujemy ponownie tę samą scenę
        Time.timeScale = 1f;  // Resetujemy czas gry
    }

    // Aktualizowanie liczby zebranych przedmiotów (np. owoców)
    public void UpdateCollectibles(int amount)
    {
        CollectiblesText.text = amount.ToString();  // Wyświetlamy liczbę przedmiotów
    }

    // Funkcja obsługująca pauzowanie gry
    public void PauseGame()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);  // Włączamy ekran pauzy
            Time.timeScale = 0f;  // Zatrzymujemy czas gry
        }
        else
        {
            pauseScreen.SetActive(false);  // Wyłączamy ekran pauzy
            Time.timeScale = 1f;  // Wznawiamy czas gry
        }
    }

    // Funkcja przejścia do głównego menu
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);  // Ładujemy scenę głównego menu
        Time.timeScale = 1f;  // Resetujemy czas gry
    }

    // Funkcja do zamknięcia aplikacji
    public void ExitGame()
    {
        Application.Quit();  // Zamyka grę
        Debug.Log("Quit");  // Wypisuje w konsoli informację o zamknięciu gry
    }

    // Funkcja zaczynająca efekt przejścia od czerni
    public void FadeFromBlack()
    {
        fadingToBlack = false;
        fadingFromBlack = true;  // Rozpoczynamy efekt przejścia z czerni
    }

    // Funkcja zaczynająca efekt przejścia do czerni
    public void FadeToBlack()
    {
        fadingToBlack = true;  // Rozpoczynamy efekt przejścia do czerni
        fadingFromBlack = false;
    }
}
