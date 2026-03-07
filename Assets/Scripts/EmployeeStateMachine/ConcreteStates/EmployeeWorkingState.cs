using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeWorkingState : EmployeeState
{
    // When working, the employee animation bobs up and down

    public EmployeeWorkingState(Employee employee, EmployeeStateMachine employeeStateMachine) : base(employee, employeeStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Employee.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
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
