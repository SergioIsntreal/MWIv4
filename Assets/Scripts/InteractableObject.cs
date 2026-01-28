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

    public Vector3 GetWalkToPoint()
    {
        return waypoint.position;
    }

    // This is called when the employee finishes walking
    public void StartInteraction()
    {
        Debug.Log("Starting " + type + " task. Will take " + timeToComplete + " seconds.");
        // We will trigger the employee's timer here
    }
}
