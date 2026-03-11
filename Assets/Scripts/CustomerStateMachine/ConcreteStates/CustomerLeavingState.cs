using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLeavingState : CustomerState
{
    Customer _customer;
    CustomerStateMachine _customerStateMachine;

    // When the customer has paid (or has run out of patience) they will leave the bistro
    public CustomerLeavingState(Customer customer, CustomerStateMachine customerStateMachine) : base(customer, customerStateMachine)
    {
        _customer = customer;
        _customerStateMachine = customerStateMachine;
    }

    public override void AnimationTriggerEvent(Customer.AnimationTriggerType cTriggerType)
    {
        base.AnimationTriggerEvent(cTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        _customer.currentState = Customer_State.Leaving;
        _customer.customerPatience.UpdateBubbleVisibility();
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
