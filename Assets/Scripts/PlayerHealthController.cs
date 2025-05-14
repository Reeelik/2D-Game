using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance; // Singleton do dostępu z innych skryptów

    private void Awake()
    {
        instance = this; // Inicjalizacja instancji singletona
    }

    public int currentHealth; // Aktualna ilość zdrowia gracza
    public int maxHealth; // Maksymalna ilość zdrowia gracza
    
    public float invincibilityLength = 1f; // Czas trwania nietykalności po otrzymaniu obrażeń
    private float invincibilityCounter; // Licznik do śledzenia czasu nietykalności

    public SpriteRenderer theSR; // Komponent do zmiany koloru sprite'a gracza
    public Color normalColor; // Normalny kolor gracza
    public Color FadeColor; // Kolor po otrzymaniu obrażeń (efekt "flashing")

    private PlayerController thePlayer; // Komponent do kontroli ruchu gracza
    
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GetComponent<PlayerController>(); // Pobranie komponentu PlayerController
        
        currentHealth = maxHealth; // Ustawienie zdrowia na maksymalne na początku gry
        
        UlController.instance.UpdateHealthDisplay(currentHealth, maxHealth); // Zaktualizowanie wyświetlania zdrowia
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCounter > 0) // Sprawdzenie, czy trwa nietykalność
        {
            invincibilityCounter -= Time.deltaTime; // Odliczanie czasu nietykalności

            if (invincibilityCounter <= 0) // Po upływie czasu nietykalności, przywrócenie normalnego koloru
            {
                theSR.color = normalColor;
            }
        }

#if UNITY_EDITOR // Możliwość testowania zdrowia tylko w edytorze
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddHealth(1); // Dodanie 1 punktu zdrowia po naciśnięciu klawisza H
        }
#endif        
    }

    // Funkcja do uszkodzenia gracza
    public void DamagePlayer()
    {
        if (invincibilityCounter <= 0) // Gracz nie może zostać uszkodzony, jeśli jest w stanie nietykalności
        {
            currentHealth--; // Zmniejszenie zdrowia gracza

            if (currentHealth <= 0) // Jeśli zdrowie gracza spadnie do 0, gracz umiera
            {
                currentHealth = 0; // Ustawienie zdrowia na 0
                LifeController.instance.Respawn(); // Respawn gracza
                
                //AudioManager.instance.PlayAllSFX(11); // Odtwarzanie dźwięku śmierci (zakomentowane)
            }
            else
            {
                invincibilityCounter = invincibilityLength; // Resetowanie licznika nietykalności
                theSR.color = FadeColor; // Zmiana koloru na efekt "flashing" po otrzymaniu obrażeń
                thePlayer.KnockBack(); // Odepchnięcie gracza w wyniku obrażeń
                AudioManager.instance.PlayAllSFX(13); // Odtwarzanie dźwięku obrażeń
            }

            UlController.instance.UpdateHealthDisplay(currentHealth, maxHealth); // Aktualizacja wyświetlania zdrowia
        }
    }

    // Funkcja do dodawania zdrowia graczowi
    public void AddHealth(int amountToAdd)
    {
        currentHealth += amountToAdd; // Dodanie zdrowia

        if (currentHealth > maxHealth) // Jeśli zdrowie przekroczy maksymalną wartość, ustawiamy je na maksymalne
        {
            currentHealth = maxHealth;
        }
        
        UlController.instance.UpdateHealthDisplay(currentHealth, maxHealth); // Aktualizacja wyświetlania zdrowia
    }
}
