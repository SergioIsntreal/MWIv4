using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeFightingState : EmployeeState
{
    // When fighting, the Employee is locked in combat for a set duration, which will override the bistro manager when it tries to call them

    public EmployeeFightingState(Employee employee, EmployeeStateMachine employeeStateMachine) : base(employee, employeeStateMachine)
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
