using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomerEnteringState : CustomerState
{
    Customer _customer;
    CustomerStateMachine _customerStateMachine;

    public CustomerEnteringState(Customer customer, CustomerStateMachine customerStateMachine) : base(customer, customerStateMachine)
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
        _customer.currentState = Customer_State.Entering;
        _customer.MoveToWaitingArea();
        _customer.aiLerp.canMove = true;
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        float dist = Vector3.Distance(_customer.transform.position, _customer.myTarget.transform.position);

        if (dist < 0.2f)
        {
            Debug.Log($"[Customer] {_customer.gameObject.name} has reached a Chair. Switching State to WAITING(CHAIR).");
            _customerStateMachine.ChangeState(_customer.WaitingChairState);
        }

        base.FrameUpdate();
    }
}
