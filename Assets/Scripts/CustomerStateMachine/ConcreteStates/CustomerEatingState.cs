using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEatingState : CustomerState
{
    // Lasts for about 6 seconds, then the customer will either return to "WaitingTable" or change to "WaitingToPay"
    // NOTE: Might need to split "WaitingTable" to 'WaitingToOrder' and 'WaitingForFood', though the animation
    // will not differ

    public CustomerEatingState(Customer customer, CustomerStateMachine customerStateMachine) : base(customer, customerStateMachine)
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
