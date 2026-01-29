using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [Header("References")]
    public TimeManager timeManager; // Drag the TimeManager object here

    [Header("Variety")]
    // Drag multiple different customer prefabs into this list in the Inspector
    public List<GameObject> customerPrefabs = new List<GameObject>();

    [Header("Randomized Timing")]
    public float minSpawnInterval = 5f;  // Minimum seconds between customers
    public float maxSpawnInterval = 15f; // Maximum seconds between customers

    private float nextSpawnTime;
    private float timer;
    private bool firstSpawnDone = false;

    void Start()
    {
        // FORCE FIX: First customer comes quickly (0 to 3 seconds)
        nextSpawnTime = Random.Range(0f, 3f);
        timer = 0f;
    }

    void Update()
    {
        // Safety Check: If TimeManager is missing or Bistro is closed, do nothing
        if (timeManager == null || !timeManager.IsBistroOpen()) return;

        timer += Time.deltaTime;

        if (timer >= nextSpawnTime)
        {
            SpawnRandomCustomer();

            // Reset timer
            timer = 0;

            // Set next interval. If it was the first spawn, switch to normal intervals.
            if (!firstSpawnDone)
            {
                firstSpawnDone = true;
            }

            // Set the random time for the NEXT customer
            nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnRandomCustomer()
    {
        if (customerPrefabs.Count == 0) return;

        // Pick a random index from our list
        int randomIndex = Random.Range(0, customerPrefabs.Count);
        Instantiate(customerPrefabs[randomIndex], transform.position, Quaternion.identity);

        Debug.Log($"A new customer has arrived!");
    }

    void SetRandomNextSpawn()
    {
        // Pick a random float between our min and max
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
