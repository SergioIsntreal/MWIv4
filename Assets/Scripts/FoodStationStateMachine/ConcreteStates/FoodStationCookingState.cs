using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStationCookingState : FoodStationState
{
    // Enters this state when an employee is "Working" and standing at the designated waypoint (or collider)
    public FoodStationCookingState(FoodStation foodStation, FoodStationStateMachine foodStationStateMachine) : base(foodStation, foodStationStateMachine)
    {
    }

    public override void AnimationTriggerEvent(FoodStation.AnimationTriggerType fsTriggerType)
    {
        base.AnimationTriggerEvent(fsTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
}
