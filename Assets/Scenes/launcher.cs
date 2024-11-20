using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher : MonoBehaviour
{
    public GameObject patient;
    public GameObject[] spawnedPatients; // Array to store spawned patients
    private Vector3[] spawnPositions;
    public GameHandler gamehandler;

    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject secondCircle;

    void Start()
    {
        Camera mainCamera = Camera.main;

        // Calculate the screen bounds in world space
        Vector2 screenBounds = GetCameraBounds(mainCamera);

        // Generate random spawn positions within the camera bounds
        spawnPositions = GenerateRandomSpawnPositions(3, screenBounds);

        spawnedPatients = new GameObject[spawnPositions.Length];
        SpawnObjects();
    }

    private Vector2 GetCameraBounds(Camera camera)
    {
        float cameraHeight = 2f * camera.orthographicSize; // Visible height
        float cameraWidth = cameraHeight * camera.aspect;  // Visible width
        return new Vector2(cameraWidth / 2f, cameraHeight / 2f); // Half extents
    }

    private Vector3[] GenerateRandomSpawnPositions(int count, Vector2 bounds)
    {
        Vector3[] positions = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            bool positionValid = false;
            Vector3 newPosition = Vector3.zero;

            while (!positionValid)
            {
                float randomX = UnityEngine.Random.Range(-bounds.x, bounds.x); // X within bounds
                float randomY = UnityEngine.Random.Range(-bounds.y, bounds.y); // Y within bounds
                float z = 0; // Adjust Z if necessary for 3D perspective
                newPosition = new Vector3(randomX, randomY, z);

                // Check distance against already generated positions
                positionValid = true; // Assume valid unless proven otherwise
                for (int j = 0; j < i; j++)
                {
                    if (Vector3.Distance(newPosition, positions[j]) < 10f) // Minimum distance is 10 units
                    {
                        positionValid = false;
                        break;
                    }
                }
            }

            positions[i] = newPosition; // Save the valid position
        }
        return positions;
    }



    public void SpawnObjects()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            if (patient != null)
            {
                // Instantiate the patient prefab
                GameObject newPatient = Instantiate(patient, spawnPositions[i], Quaternion.identity);
                Patient patientScript = newPatient.GetComponent<Patient>();
                if (patientScript != null)
                {
                    // Assign a random health value
                    patientScript.health = UnityEngine.Random.Range(50f, 100f);

                    // Assign the distance from this patient to the secondCircle
                    if (secondCircle != null)
                    {
                        float distance = Vector3.Distance(newPatient.transform.position, secondCircle.transform.position);
                        patientScript.distance = distance; // Store the calculated distance
                    }
                }

                // Store the spawned patient in the array
                spawnedPatients[i] = newPatient;

                // Assign the prefab to the points based on its order
               
                UnityEngine.Debug.Log("Object 1 has the highest priority");
            }
        }
      
    }
    
}
