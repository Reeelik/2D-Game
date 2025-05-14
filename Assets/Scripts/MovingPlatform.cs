using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform thePlatform; // Transform platformy, którą będziemy poruszać
    public Transform[] movePoints; // Tablica punktów, do których platforma będzie się poruszać

    private int currentPoint; // Indeks aktualnego punktu docelowego
    public float moveSpeed; // Prędkość poruszania platformy

    // Start is called before the first frame update
    void Start()
    {
        // Inicjalizacja (niepotrzebna w tej wersji, ale może być użyteczna w przyszłości)
    }

    // Update is called once per frame
    void Update()
    {
        // Przemieszczanie platformy do aktualnego punktu docelowego
        thePlatform.position = Vector3.MoveTowards(thePlatform.position, movePoints[currentPoint].position, moveSpeed * Time.deltaTime);

        // Sprawdzanie, czy platforma osiągnęła aktualny punkt docelowy
        if (thePlatform.position == movePoints[currentPoint].position)
        {
            currentPoint++; // Przechodzimy do następnego punktu docelowego

            // Jeśli osiągnięto ostatni punkt, wracamy do pierwszego punktu
            if (currentPoint >= movePoints.Length)
            {
                currentPoint = 0;
            }
        }
    }
}