using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public string mainMenu;  // Zmienna przechowująca nazwę sceny głównego menu

    // Start jest wywoływane przed pierwszą klatką
    void Start()
    {
        // Usuwamy wszystkie dane zapisane przez gracza w PlayerPrefs
        // Może być użyteczne do resetowania postępu przed wyświetleniem ekranu zwycięstwa
        PlayerPrefs.DeleteAll();
    }

    // Funkcja uruchamiana przy kliknięciu przycisku "MainMenu"
    public void MainMenu()
    {
        // Ładuje scenę głównego menu na podstawie zmiennej mainMenu
        SceneManager.LoadScene(mainMenu);
    }

    // Funkcja uruchamiana przy kliknięciu przycisku "QuitGame"
    public void QuitGame()
    {
        // Zamyka aplikację (działa tylko w wersjach zbudowanych, nie w edytorze)
        Application.Quit();
    }
}