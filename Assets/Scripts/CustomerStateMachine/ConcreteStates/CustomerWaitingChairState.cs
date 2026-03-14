using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CustomerWaitingChairState : CustomerState
{
    [HideInInspector] public AILerp aiLerp;
    private float currentPatience;
    DragNDrop dragNDrop;
    Customer _customer;
    CustomerStateMachine _customerStateMachine;

    public CustomerWaitingChairState(Customer customer, CustomerStateMachine customerStateMachine) : base(customer, customerStateMachine)
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

        _customer.currentState = Customer_State.Waiting;
        _customer.aiLerp.autoRepath.mode = AutoRepathPolicy.Mode.Never;
        _customer.aiLerp.canMove = false;
        currentPatience = _customer.maxPatience;
    }

    private void Update()
    {
        // Was this line impacting the chair manager???
        //aiLerp.canMove = false;
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
            // Customer leaves if patience runs out - change state
        }
    }
}
