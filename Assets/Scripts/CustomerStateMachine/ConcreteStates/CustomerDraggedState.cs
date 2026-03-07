using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDraggedState : CustomerState
{
    // When being dragged, the customer's patience timer is paused. If they are dragged to an empty table,
    // it will switch to the "WaitingTable" state, and if not, then return to "WaitingChair" state, timer resumes
    public CustomerDraggedState(Customer customer, CustomerStateMachine customerStateMachine) : base(customer, customerStateMachine)
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
