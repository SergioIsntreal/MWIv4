using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    // EXPERIMENT: STATE MACHINES
    #region State Machine Variables

    public TableStateMachine tStateMachine { get; set; }
    public TableEmptyCleanState EmptyCleanState { get; set; }
    public TableOccupiedState OccupiedState { get; set; }
    public TableEmptyDirtyState EmptyDirtyState { get; set; }

    private void AnimationTriggerEvent(AnimationTriggerType tTriggerType)
    {
        tStateMachine.CurrentTableState.AnimationTriggerEvent(tTriggerType);
    }

    public enum AnimationTriggerType
    {
        EmptyClean,
        Occupied,
        EmptyDirty
    }

    private void Awake()
    {
        tStateMachine = new TableStateMachine();
        EmptyCleanState = new TableEmptyCleanState(this, tStateMachine);
        OccupiedState = new TableOccupiedState(this, tStateMachine);
        EmptyDirtyState = new TableEmptyDirtyState(this, tStateMachine);
    }

    private void Start()
    {
        tStateMachine.Initialize(EmptyCleanState);
    }

    private void Update()
    {
        tStateMachine.CurrentTableState.FrameUpdate();
    }

    #endregion
}
