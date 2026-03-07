using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStationEmptyState : FoodStationState
{
    public FoodStationEmptyState(FoodStation foodStation, FoodStationStateMachine foodStationStateMachine) : base(foodStation, foodStationStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Employee.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
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
