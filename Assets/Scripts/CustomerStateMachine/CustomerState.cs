using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerState
{
    protected Customer customer;
    protected CustomerStateMachine customerStateMachine;

    public CustomerState(Customer customer, CustomerStateMachine customerStateMachine)
    {
        this.customer = customer;
        this.customerStateMachine = customerStateMachine;
    }

    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    public virtual void FrameUpdate()
    {

    }

    public virtual void AnimationTriggerEvent(Customer.AnimationTriggerType cTriggerType)
    {

    }
}
