using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // A list of all possible types of interaction
    public enum InteractionType
    {
        TakingOrder, MakingFood, OperatingTill, CleaningTable, FightingHunter
    }

    [Header("Settings")]
    public InteractionType type;
    public float timeToComplete = 3.0f; // Can be set individually in the Inspector
    
    // Apply to any object in which an employee will interact with
    public Transform waypoint;

    private FoodStation foodStation;
    void Awake()
    {
        // Cache the food station script if this is a cooking station
        foodStation = GetComponent<FoodStation>();
    }

    public Vector3 GetWalkToPoint() => waypoint.position;

    // This is called when the employee finishes walking
    public bool StartInteraction()
    {
        if (type == InteractionType.MakingFood && foodStation != null)
        {
            if (foodStation.hasFoodReady)
            {
                Debug.Log("Station already has food! Cannot make more.");
                return false; // Tell the employee "No"
            }
        }

        Debug.Log($"Starting {type}. Takes {timeToComplete}s.");
        return true; // Tell the employee "Yes, start your timer"
    }

    public void CompleteInteraction()
    {
        if (type == InteractionType.MakingFood && foodStation != null)
        {
            foodStation.FinishCooking();
        }
        // Add other completion logic for Till, Tables, etc.
    }
}
