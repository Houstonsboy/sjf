using UnityEngine;
using Health;
using System;
using System.Diagnostics;

public class GameHandler : MonoBehaviour
{
    public launcher spawner; // Reference to the SimpleSpawner script
    static bool[] flag = new bool[3];
    static int turn;
   

    // References to the prefabs
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;

    void Start()
    {
        if (spawner == null)
        {
            UnityEngine.Debug.LogError("launcher is not assigned!");
        }

        // Optionally, instantiate prefabs at the points initially
        
    }

    void Update()
    {
        sjf();
    }

    // SJF function with process priority logic
    public void sjf()
    {
        if (spawner == null || spawner.spawnedPatients == null) return;

        // Fetch health values of the spawned patients
        int health1 = GetHealth(spawner.spawnedPatients[0]);
        int health2 = GetHealth(spawner.spawnedPatients[1]);
        int health3 = GetHealth(spawner.spawnedPatients[2]);

        int distance1 = GetDistance(spawner.spawnedPatients[0]);
        int distance2 = GetDistance(spawner.spawnedPatients[1]);
        int distance3 = GetDistance(spawner.spawnedPatients[2]);

        // Assuming distances are fixed
       

        // Calculate priorities
        float priority1 = health1 * distance1;
        float priority2 = health2 * distance2;
        float priority3 = health3 * distance3;

        UnityEngine.Debug.Log($"Priority 1: {priority1}, Priority 2: {priority2}, Priority 3: {priority3}");

        // Determine the process with the highest priority
        if (priority1 <= priority2 && priority1 <= priority3)
        {
            UnityEngine.Debug.Log("Object 1 has the highest priority");
            ProcessRequest(1);
        }
        else if (priority2 <= priority1 && priority2 <= priority3)
        {
            UnityEngine.Debug.Log("Object 2 has the highest priority");
            ProcessRequest(2);
        }
        else
        {
            UnityEngine.Debug.Log("Object 3 has the highest priority");
            ProcessRequest(3);
        }

        // After processing, calculate and log the distances
        
    }

    // Helper method to get health from a patient
    private int GetHealth(GameObject patient)
    {
        if (patient != null)
        {
            Patient patientScript = patient.GetComponent<Patient>();
            if (patientScript != null)
            {
                return (int)patientScript.health;
            }
        }
        return 0;
    }
    private int GetDistance(GameObject patient)
    {
        if (patient != null)
        {
            Patient patientScript = patient.GetComponent<Patient>();
            if (patientScript != null)
            {
                return (int)patientScript.distance;
            }
        }
        return 0;
    }

    // Handle process requests based on the priority
    public void ProcessRequest(int processId)
    {
        switch (processId)
        {
            case 1: Process1(); break;
            case 2: Process2(); break;
            case 3: Process3(); break;
        }
    }

    static void Process1()
    {
        flag[0] = true;
        turn = 1;
        while ((flag[1] || flag[2]) && turn == 1) { }
        UnityEngine.Debug.Log("Process 1 is accessing the resource");
        flag[0] = false;
    }

    static void Process2()
    {
        flag[1] = true;
        turn = 2;
        while ((flag[0] || flag[2]) && turn == 2) { }
        UnityEngine.Debug.Log("Process 2 is accessing the resource");
        flag[1] = false;
    }

    static void Process3()
    {
        flag[2] = true;
        turn = 0;
        while ((flag[0] || flag[1]) && turn == 0) { }
        UnityEngine.Debug.Log("Process 3 is accessing the resource");

        flag[2] = false;
    }

    // Method to instantiate a prefab at a given point
    private void InstantiatePrefabAtPoint(GameObject point, GameObject prefab)
    {
        if (point != null && prefab != null)
        {
            Instantiate(prefab, point.transform.position, Quaternion.identity);
        }
    }

    // Method to calculate and log distance from the fixed point
    
}
