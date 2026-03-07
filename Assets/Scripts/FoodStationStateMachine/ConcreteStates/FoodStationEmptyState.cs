using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStationEmptyState : FoodStationState
{
    // Default state; not being interacted with
    public FoodStationEmptyState(FoodStation foodStation, FoodStationStateMachine foodStationStateMachine) : base(foodStation, foodStationStateMachine)
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
