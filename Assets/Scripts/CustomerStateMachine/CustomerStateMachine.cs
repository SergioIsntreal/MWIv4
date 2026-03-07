using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStateMachine
{
    public CustomerState CurrentCustomerState { get; set; }

    public void Initialize(CustomerState cStartingState)
    {
        CurrentCustomerState = cStartingState;
        CurrentCustomerState.EnterState();
    }

    public void ChangeState(CustomerState cNewState)
    {
        CurrentCustomerState.ExitState();
        CurrentCustomerState = cNewState;
        CurrentCustomerState.EnterState();
    }
}
