using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStationState
{
    protected FoodStation foodStation;
    protected FoodStationStateMachine foodStationStateMachine;

    public FoodStationState(FoodStation foodStation, FoodStationStateMachine foodStationStateMachine)
    {
        this.foodStation = foodStation;
        this.foodStationStateMachine = foodStationStateMachine;
    }

    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    public virtual void FrameUpdate()
    {

    }

    public virtual void AnimationTriggerEvent(FoodStation.AnimationTriggerType fsTriggerType)
    {

    }
}
