using System;
using UnityEngine;

public class UnitIdleState : UnitBaseState
{
    private int idleHash = Animator.StringToHash("Idle");
    public override void EnterState(Unit unit)
    {
        unit.PlayAnimation(idleHash);
        unit.SetVelocity(true);
    }

    public override void ExitState(Unit unit)
    {
        Debug.Log("Exit Idle State");
    }

    public override void FixedUpdateState(Unit unit)
    {
    }

    public override void UpdateState(Unit unit)
    {
        Collider[] colliders =  Physics.OverlapBox(unit.attackRange.transform.position, unit.attackRange.size/2, Quaternion.identity,LayerMask.GetMask("Unit"));
        if(colliders.Length > 1)
        {
            unit.SwitchState(unit.unitAttackState);
        }
    }
}
