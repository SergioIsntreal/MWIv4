using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStationFullState : FoodStationState
{
    // When there is a dish waiting at the station, you cannot make more until it is removed
    public FoodStationFullState(FoodStation foodStation, FoodStationStateMachine foodStationStateMachine) : base(foodStation, foodStationStateMachine)
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
