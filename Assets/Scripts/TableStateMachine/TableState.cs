using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableState
{
    protected Table table;
    protected TableStateMachine tableStateMachine;

    public TableState(Table table, TableStateMachine tableStateMachine)
    {
        this.table = table;
        this.tableStateMachine = tableStateMachine;
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

    public virtual void AnimationTriggerEvent(Table.AnimationTriggerType tTriggerType)
    {

    }
}
