using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaitingChairState : CustomerState
{

    [HideInInspector] public AILerp aiLerp;
    private float currentPatience;
    DragNDrop dragNDrop;
    Customer _customer;
    CustomerStateMachine _customerStateMachine;

    // Waiting for the player to drag them to an empty table. When dragged, enters "DraggedState"
    public CustomerWaitingChairState(Customer customer, CustomerStateMachine customerStateMachine) : base(customer, customerStateMachine)
    {
        _customer = customer;
        _customerStateMachine = customerStateMachine;
    }

    public override void AnimationTriggerEvent(Customer.AnimationTriggerType cTriggerType)
    {
        base.AnimationTriggerEvent(cTriggerType);
    }

    private void Update()
    {
        aiLerp.canMove = false;
    }

    public override void EnterState()
    {
        base.EnterState();
        _customer.currentState = Customer_State.Waiting;
        currentPatience = _customer.maxPatience;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        currentPatience -= Time.deltaTime;

        if (currentPatience <= 0)
        {
            _customerStateMachine.ChangeState(_customer.LeavingState);
        }
    }
}
