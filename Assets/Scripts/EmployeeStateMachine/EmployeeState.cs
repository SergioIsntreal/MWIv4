using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeState
{
    protected Employee employee;
    protected EmployeeStateMachine employeeStateMachine;

    public EmployeeState(Employee employee, EmployeeStateMachine employeeStateMachine)
    {
        this.employee = employee;
        this.employeeStateMachine = employeeStateMachine;
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

    public virtual void AnimationTriggerEvent(Employee.AnimationTriggerType eTriggerType)
    {

    }
}
