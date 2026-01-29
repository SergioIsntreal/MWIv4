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

    void Start()
    {
        // Set the very first spawn time
        SetRandomNextSpawn();
    }

    void Update()
    {
        // Only run logic if the bistro is actually open
        if (timeManager != null && timeManager.IsBistroOpen())
        {
            timer += Time.deltaTime;

            if (timer >= nextSpawnTime)
            {
                SpawnRandomCustomer();
                timer = 0;
                SetRandomNextSpawn(); // Pick a new random time for the next person
            }
        }
    }

    void SpawnRandomCustomer()
    {
        if (customerPrefabs.Count == 0) return;

        // Pick a random index from our list
        int randomIndex = Random.Range(0, customerPrefabs.Count);
        GameObject selectedPrefab = customerPrefabs[randomIndex];

        // Instantiate at the spawner's position
        Instantiate(selectedPrefab, transform.position, Quaternion.identity);

        Debug.Log($"A new {selectedPrefab.name} has arrived!");
    }

    void SetRandomNextSpawn()
    {
        // Pick a random float between our min and max
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
