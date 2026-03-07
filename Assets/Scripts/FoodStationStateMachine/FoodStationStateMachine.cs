using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStationStateMachine
{
    public FoodStationState CurrentFoodStationState { get; set; }

    public void Initialize(FoodStationState fsStartingState)
    {
        CurrentFoodStationState = fsStartingState;
        CurrentFoodStationState.EnterState();
    }

    public void ChangeState(FoodStationState fsNewState)
    {
        CurrentFoodStationState.ExitState();
        CurrentFoodStationState = fsNewState;
        CurrentFoodStationState.EnterState();
    }
}
