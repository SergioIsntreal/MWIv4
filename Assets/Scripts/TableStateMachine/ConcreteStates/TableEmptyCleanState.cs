using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableEmptyCleanState : TableState
{
    public TableEmptyCleanState(Table table, TableStateMachine tableStateMachine) : base(table, tableStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Table.AnimationTriggerType tTriggerType)
    {
        base.AnimationTriggerEvent(tTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
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
