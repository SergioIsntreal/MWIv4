using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingAreaManager : MonoBehaviour
{
    public static WaitingAreaManager Instance;
    public List<Transform> waitingSlots; // Drag chair transformers here

    // Using Transform as key to track if a chair is taken
    private Dictionary<Transform, bool> slotOccupancy = new Dictionary<Transform, bool>();

    void Awake()
    {
        Instance = this;

        // Initialize the dictionary
        foreach (var slot in waitingSlots)
        {
            if (slot != null)
            {
                slotOccupancy[slot] = false;
            }
        }
    }

    public Transform GetClosestSlot(Vector3 entrancePosition)
    {
        Transform bestSlot = null;
        float closestDistance = float.MaxValue; // Start with an infinitely high number

        foreach (var slot in waitingSlots)
        {
            // 1. Check if the slot exists and is empty
            if (slot != null && !slotOccupancy[slot])
            {
                // 2. Measure distance from the entrance to this specific chair
                float distance = Vector3.Distance(entrancePosition, slot.position);

                // 3. If this chair is closer than the last one we found, pick it!
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestSlot = slot;
                }
            }
        }

        // 4. If we found a valid chair, mark it as taken and return it
        if (bestSlot != null)
        {
            slotOccupancy[bestSlot] = true;
        }

        return bestSlot;
    }

    public void ReleaseSlot(Transform slot)
    {
        if (slot != null && slotOccupancy.ContainsKey(slot))
        {
            slotOccupancy[slot] = false;
        }   
    }
}
