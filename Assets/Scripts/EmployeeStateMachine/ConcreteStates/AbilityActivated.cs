using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivated : EmployeeState
{
    // This is a momentary state, where an event is triggered and then the employee is reset back to idle

    public AbilityActivated(Employee employee, EmployeeStateMachine employeeStateMachine) : base(employee, employeeStateMachine)
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
