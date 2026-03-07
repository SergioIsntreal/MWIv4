using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaitingChairState : CustomerState
{
    // Waiting for the player to drag them to an empty table. When dragged, enters "DraggedState"
    public CustomerWaitingChairState(Customer customer, CustomerStateMachine customerStateMachine) : base(customer, customerStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Customer.AnimationTriggerType cTriggerType)
    {
        base.AnimationTriggerEvent(cTriggerType);
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
