using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStateMachine
{
    public EmployeeState CurrentEmployeeState { get; set; }

    public void Initialize(EmployeeState eStartingState)
    {
        CurrentEmployeeState = eStartingState;
        CurrentEmployeeState.EnterState();
    }

    public void ChangeState(EmployeeState eNewState)
    {
        CurrentEmployeeState.ExitState();
        CurrentEmployeeState = eNewState;
        CurrentEmployeeState.EnterState();
    }
}
