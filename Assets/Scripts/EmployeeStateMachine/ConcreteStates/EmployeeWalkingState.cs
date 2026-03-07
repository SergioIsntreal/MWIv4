using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeWalkingState : EmployeeState
{
    // When walking, I have  a handle flipper that affects which direction they face when walking

    public EmployeeWalkingState(Employee employee, EmployeeStateMachine employeeStateMachine) : base(employee, employeeStateMachine)
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
