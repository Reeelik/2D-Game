using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;

public class CameraController : MonoBehaviour  
{  
    public Transform target; // Obiekt, na który kamera ma się kierować  
    public bool freezeVertical; // Czy zablokować ruch kamery w osi pionowej  
    public bool freezeHorizontal; // Czy zablokować ruch kamery w osi poziomej  
    private Vector3 positionStore; // Przechowuje pierwotną pozycję kamery  

    public bool clampPosition; // Czy ograniczyć pozycję kamery do określonego obszaru  

    public Transform clampMin; // Minimalne granice ograniczenia kamery  
    public Transform clampMax; // Maksymalne granice ograniczenia kamery  

    private float halfWidth; // Połowa szerokości ekranu kamery  
    private float halfHeight; // Połowa wysokości ekranu kamery  

    public Camera theCam; // Odniesienie do obiektu kamery  

    void Start()  
    {  
        positionStore = transform.position; // Zapamiętanie początkowej pozycji kamery  

        clampMin.SetParent(null); // Odłączenie granic ograniczeń od rodzica  
        clampMax.SetParent(null);  

        halfHeight = theCam.orthographicSize; // Obliczenie połowy wysokości kamery  
        halfWidth = theCam.orthographicSize * theCam.aspect; // Obliczenie połowy szerokości kamery  
    }  

    void LateUpdate()  
    {  
        transform.position = new Vector3(target.position.x, target.position.y, -10); // Ustawienie pozycji kamery na pozycję celu  

        if (freezeVertical)  
        {  
            transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z); // Zablokowanie ruchu w osi Y  
        }  

        if (freezeHorizontal)  
        {  
            transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z); // Zablokowanie ruchu w osi X  
        }  

        if (clampPosition)  
        {  
            transform.position = new Vector3(  
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth), // Ograniczenie w osi X  
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight), // Ograniczenie w osi Y  
                transform.position.z  
            );  
        }  

        if (ParallaxBackground.instance != null)  
        {  
            ParallaxBackground.instance.MoveBackground(); // Obsługa paralaksy tła, jeśli istnieje  
        }  
    }  

    private void OnDrawGizmos() // Rysuje linie określające granice ograniczeń w edytorze  
    {  
        if (clampPosition)  
        {  
            Gizmos.color = Color.magenta;  
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f)); // Linia pionowa z lewej  
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f)); // Linia pozioma na dole  

            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f)); // Linia pozioma na górze  
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f)); // Linia pionowa z prawej  
        }  
    }  
}  
