using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager instance; // Statyczna instancja menedżera zbieranych przedmiotów

    // Inicjalizacja instancji CollectiblesManager
    public void Awake()
    {
        instance = this;
    }

    public int collectibleCount; // Liczba zebranych przedmiotów
    public int extraLifeTreshold; // Próg do zdobycia dodatkowego życia
    
    // Start is called before the first frame update
    void Start()
    {
        // Ustawienie początkowej liczby przedmiotów na wartość przechowywaną w InfoTracker
        collectibleCount = InfoTracker.instance.currentFruit;

        // Jeśli instancja UlController istnieje, zaktualizuj UI z liczbą przedmiotów
        if (UlController.instance != null)
        {
            UlController.instance.UpdateCollectibles(collectibleCount);
        }
    }
    // Funkcja dodająca przedmioty do kolekcji
    public void GetCollectibles(int amount)
    {
        collectibleCount += amount; // Zwiększenie liczby przedmiotów

        // Sprawdzenie, czy liczba przedmiotów osiągnęła próg dodatkowego życia
        if (collectibleCount >= extraLifeTreshold)
        {
            collectibleCount -= extraLifeTreshold; // Odjęcie przedmiotów odpowiadających za dodatkowe życie

            // Jeśli instancja LifeController istnieje, dodaj życie
            if (LifeController.instance != null)
            {
                LifeController.instance.AddLife();  
            }
        }

        // Zaktualizowanie liczby przedmiotów w UI
        if (UlController.instance != null)
        {
            UlController.instance.UpdateCollectibles(collectibleCount);
        }
    }
}