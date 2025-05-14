using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreelineMover : MonoBehaviour
{
    // Maksymalna odległość, jaką może przebyć treeline od kamery
    public float maxDistance = 22f;

    // Update jest wywoływane raz na klatkę
    void Update()
    {
        // Obliczamy odległość pomiędzy treeline a kamerą na osi X
        float distance = transform.position.x - Camera.main.transform.position.x;

        // Jeśli treeline jest za daleko na prawo, przesuwamy go w lewo
        if (distance > maxDistance)
        {
            transform.position -= new Vector3(maxDistance * 2f, 0f, 0f);
        }
        // Jeśli treeline jest za daleko na lewo, przesuwamy go w prawo
        else if (distance < -maxDistance)
        {
            transform.position += new Vector3(maxDistance * 2f, 0f, 0f);
        }
    }
}