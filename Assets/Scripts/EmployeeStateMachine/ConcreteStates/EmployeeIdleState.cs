using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeIdleState : EmployeeState
{
    // When idle, the Employee does not move (May have a lineart boil animation)

    public EmployeeIdleState(Employee employee, EmployeeStateMachine employeeStateMachine) : base(employee, employeeStateMachine)
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
