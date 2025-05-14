using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlMusicPlayer : MonoBehaviour
{
    public int trackToPlay; // Indeks utworu, który ma być odtworzony

    // Start is called before the first frame update
    void Start()
    {
        // Sprawdzamy, czy istnieje instancja AudioManagera
        if (AudioManager.instance != null)
        {
            // Jeśli tak, odtwarzamy muzykę poziomu wskazaną przez trackToPlay
            AudioManager.instance.PlayLvlMusic(trackToPlay);
        }
    }
}