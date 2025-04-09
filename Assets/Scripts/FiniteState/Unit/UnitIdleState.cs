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
    }

    public override void FixedUpdateState(Unit unit)
    {
    }

    public override void UpdateState(Unit unit)
    {
        if(unit.CheckIfCanAttack()){
            unit.SwitchState(unit.unitAttackState);
        }
    }
}
