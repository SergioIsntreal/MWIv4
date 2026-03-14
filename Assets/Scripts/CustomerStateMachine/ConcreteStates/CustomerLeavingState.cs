using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLeavingState : CustomerState
{
    Customer _customer;
    CustomerStateMachine _customerStateMachine;

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
        _customer.aiLerp.canMove = true;
        _customer.LeaveBistro();
        Debug.Log($"You took too long! [Customer] {_customer.gameObject.name} has stormed off!");
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
