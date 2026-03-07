using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableStateMachine
{
    public TableState CurrentTableState { get; set; }

    public void Initialize(TableState tStartingState)
    {
        CurrentTableState = tStartingState;
        CurrentTableState.EnterState();
    }

    public void ChangeState(TableState tNewState)
    {
        CurrentTableState.ExitState();
        CurrentTableState = tNewState;
        CurrentTableState.EnterState();
    }
}
