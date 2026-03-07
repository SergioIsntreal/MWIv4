using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaitingTableState : CustomerState
{
    // When the customer is seated at a table, they enter this state. The patience resets when the employee
    // takes their order, or their food is served. If the patience meter runs out, they switch to 'LeavingState'
    public CustomerWaitingTableState(Customer customer, CustomerStateMachine customerStateMachine) : base(customer, customerStateMachine)
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
