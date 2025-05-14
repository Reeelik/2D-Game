using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlExit : MonoBehaviour
{
   public Animator anim; // Animator odpowiedzialny za animacje wyjścia z poziomu
   private bool isEnding; // Flaga informująca, czy poziom jest już zakończony
   public string nextLvl; // Nazwa kolejnego poziomu do załadowania
   public float waitToEndLvl = 2f; // Czas oczekiwania przed zakończeniem poziomu
   public GameObject blocker; // Obiekt blokujący (np. ekran ładowania)
   public float fadeTime = 1f; // Czas trwania efektu znikania ekranu (fade)

   // Funkcja wywoływana, gdy gracz wchodzi w kolizję z wyjściem poziomu
   private void OnTriggerEnter2D(Collider2D other)
   {
      // Sprawdzenie, czy poziom jeszcze nie jest zakończony
      if (isEnding == false)
      {
         // Jeśli kolidujący obiekt to gracz
         if (other.CompareTag("Player"))
         {
            isEnding = true; // Ustawienie flagi, że poziom jest zakończony
            anim.SetTrigger("ended"); // Wywołanie animacji zakończenia poziomu
            
            AudioManager.instance.PlayLvlComplateMusic(); // Odtworzenie muzyki końcowej

            blocker.SetActive(true); // Aktywowanie blokera (np. ekran ładowania)

            // Rozpoczęcie procesu kończenia poziomu
            StartCoroutine(EndLvlCo());
         }
      }
   }

   // Kooperacja odpowiedzialna za proces zakończenia poziomu
   IEnumerator EndLvlCo()
   {
      // Czekanie na zakończenie animacji przed rozpoczęciem fade-out
      yield return new WaitForSeconds(waitToEndLvl - fadeTime);

      // Inicjowanie efektu fade-out
      UlController.instance.FadeToBlack();
      
      // Czekanie na zakończenie efektu fade-out
      yield return new WaitForSeconds(fadeTime);

      // Pobranie i zapisanie danych o życiu i owocach
      InfoTracker.instance.GetInfo();
      InfoTracker.instance.SaveInfo();

      // Zapisanie informacji o aktualnym poziomie
      PlayerPrefs.SetString("currentLvl", nextLvl);

      // Załadowanie kolejnego poziomu
      SceneManager.LoadScene(nextLvl);
   }
}
