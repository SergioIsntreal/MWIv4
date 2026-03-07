using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaitingToPayState : CustomerState
{
    // The customer will walk towards the till and wait for an employee to be standing behind it (NO PATIENCE METER HERE)
    // Once paid, enter 'LeavingState'
    public CustomerWaitingToPayState(Customer customer, CustomerStateMachine customerStateMachine) : base(customer, customerStateMachine)
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
