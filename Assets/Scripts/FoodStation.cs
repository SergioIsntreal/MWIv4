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

    // EXPERIMENT: STATE MACHINES
    #region State Machine Variables

    public FoodStationStateMachine fsStateMachine { get; set; }
    public FoodStationEmptyState EmptyState { get; set; }
    public FoodStationCookingState CookingState { get; set; }
    public FoodStationFullState FullState { get; set; }

    private void AnimationTriggerEvent(AnimationTriggerType fsTriggerType)
    {
        fsStateMachine.CurrentFoodStationState.AnimationTriggerEvent(fsTriggerType);
    }

    public enum AnimationTriggerType
    {
        Empty,
        Cooking,
        Full
    }

    private void Awake()
    {
        fsStateMachine = new FoodStationStateMachine();
        EmptyState = new FoodStationEmptyState(this, fsStateMachine);
        CookingState = new FoodStationCookingState(this, fsStateMachine);
        FullState = new FoodStationFullState(this, fsStateMachine);
    }

    private void Start()
    {
        fsStateMachine.Initialize(EmptyState);
    }

    private void Update()
    {
        fsStateMachine.CurrentFoodStationState.FrameUpdate();
    }

    #endregion

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
