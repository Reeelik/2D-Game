using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public static ParallaxBackground instance; // Statyczna instancja klasy, dostępna globalnie

    private void Awake()
    {
        instance = this; // Ustawienie instancji klasy
    }

    private Transform theCam; // Transform kamery, używane do uzyskiwania pozycji kamery
    public Transform sky; // Tło nieba
    public Transform treeline; // Linia drzew w tle

    [Range(0f, 1f)] // Zakres dla prędkości parallax (0 - brak efektu, 1 - pełny efekt)
    public float parallaxSpeed; // Prędkość efektu parallax

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main.transform; // Pobranie transformacji kamery głównej
    }

    
    // Funkcja odpowiedzialna za poruszanie tła
    public void MoveBackground()
    {
        // Przemieszczanie tła nieba, aby śledziło pozycję kamery
        sky.position = new Vector3(theCam.position.x, theCam.position.y, theCam.position.z);

        // Przemieszczanie linii drzew w tle z efektem parallax (porusza się wolniej)
        treeline.position = new Vector3(theCam.position.x * parallaxSpeed, theCam.position.y, treeline.position.z);
    }
}