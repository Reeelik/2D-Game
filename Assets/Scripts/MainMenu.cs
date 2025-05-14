using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel; // Nazwa poziomu, który ma zostać załadowany po rozpoczęciu gry
    public int startLives = 3; // Liczba żyć, z jaką gracz zaczyna grę
    public int startFruit = 0; // Liczba owoców (lub innych przedmiotów), z jakimi gracz zaczyna grę
    public GameObject continueButton; // Przycisk "Kontynuuj", który pojawia się, gdy gra ma zapisane dane

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMenuMusic(); // Odtwarzanie muzyki w menu głównym

        // Sprawdzenie, czy istnieje zapisany poziom w PlayerPrefs
        if (PlayerPrefs.HasKey("currentLvl"))
        {
            continueButton.SetActive(true); // Pokazuje przycisk "Kontynuuj", jeśli jest zapisany poziom
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Jeśli naciśnięto klawisz M, zmiana muzyki na utwór poziomu 1
        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioManager.instance.PlayLvlMusic(1);
        }

        // W trybie edytora, jeśli naciśnięto klawisz C, usuwanie wszystkich zapisów z PlayerPrefs
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.DeleteAll();  
        }
#endif
    }

    // Funkcja uruchamiająca nową grę
    public void StartGame()
    {
        // Ustawienie początkowych wartości żyć i owoców
        InfoTracker.instance.currentLives = startLives;
        InfoTracker.instance.currentFruit = startFruit;
        
        // Zapisanie początkowych danych
        InfoTracker.instance.SaveInfo();
        
        // Załadowanie pierwszego poziomu
        SceneManager.LoadScene(firstLevel); 
    }

    // Funkcja zamykająca grę
    public void ExitGame()
    {
        Application.Quit(); // Zamyka aplikację
        Debug.Log("Quit"); // Wypisuje informację o zakończeniu gry w konsoli
    }

    // Funkcja kontynuująca grę od ostatnio zapisanego poziomu
    public void ContinueGame()
    {
        // Załadowanie poziomu, który jest zapisany w PlayerPrefs
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLvl"));
    }
}
