using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStateMachine
{
    public EmployeeState CurrentEmployeeState { get; set; }

    public void Initialize(EmployeeState startingState)
    {
        CurrentEmployeeState = startingState;
        CurrentEmployeeState.EnterState();
    }

    public void ChangeState(EmployeeState newState)
    {
        CurrentEmployeeState.ExitState();
        CurrentEmployeeState = newState;
        CurrentEmployeeState.EnterState();
    }
}
