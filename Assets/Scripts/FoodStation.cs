using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStation : MonoBehaviour
{
    [Header("Station Settings")]
    public string foodName; // e.g., "Burger", "Soup"
    public GameObject foodPrefab; // The visual food item that appears
    public Transform spawnPoint; // Where the food sits on the counter

    [Header("State")]
    public bool hasFoodReady = false;
    private GameObject currentFoodInstance;

    // Called by the Employee when the timer finishes
    public void FinishCooking()
    {
        if (hasFoodReady) return;

        // Spawn the visual food
        currentFoodInstance = Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);
        currentFoodInstance.transform.SetParent(spawnPoint);

        hasFoodReady = true;
        Debug.Log(foodName + " is ready for pickup!");
    }
}
