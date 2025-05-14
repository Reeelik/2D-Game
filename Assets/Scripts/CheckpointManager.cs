using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] allCP; // Tablica przechowująca wszystkie punkty kontrolne
    
    private Checkpoint activeCP; // Aktywny punkt kontrolny
    public Vector3 respawnPosition; // Pozycja respawnu

    // Start is called before the first frame update
    void Start()
    {
        // Wyszukiwanie wszystkich obiektów typu Checkpoint w scenie
        allCP = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);

        // Przypisanie odniesienia do CheckpointManager do każdego punktu kontrolnego
        foreach (Checkpoint cp in allCP)
        {
            cp.cpMan = this; 
        }
        
        // Ustawienie początkowej pozycji respawnu na pozycję gracza
        respawnPosition = FindFirstObjectByType<PlayerController>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Jeśli wciśnięto klawisz "C", dezaktywuj wszystkie punkty kontrolne
        if (Input.GetKeyDown(KeyCode.C))
        {
            DeactivateAllCheckPoints();
        }
    }

    // Funkcja dezaktywująca wszystkie punkty kontrolne
    public void DeactivateAllCheckPoints()
    {
        foreach (Checkpoint cp in allCP)
        {
            cp.DeactivateCheckpoint(); // Dezaktywowanie punktu kontrolnego
        } 
    }

    // Funkcja ustawiająca nowy aktywny punkt kontrolny
    public void SetActiveCheckPoint(Checkpoint newActiveCP)
    {
        DeactivateAllCheckPoints(); // Dezaktywowanie wszystkich punktów kontrolnych
        activeCP = newActiveCP; // Ustawienie nowego aktywnego punktu kontrolnego
        
        // Ustawienie pozycji respawnu na wysokość nowego punktu kontrolnego (z offsetem w osi Y)
        respawnPosition = newActiveCP.transform.position + new Vector3(0, 5.0f, 0);
        
        //Debug.Log($"[CheckpointManager] Nowa pozycja respawnu ustawiona na: {respawnPosition}");
    }
}