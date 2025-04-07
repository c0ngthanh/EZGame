using UnityEngine;

public class UnitAttackState : UnitBaseState
{
    private int attackHash = Animator.StringToHash("Attack");
    public override void EnterState(Unit unit)
    {
        unit.PlayAnimation(attackHash);
    }

    public override void ExitState(Unit unit)
    {
        Debug.Log("Exit Attack State");
    }

    public override void FixedUpdateState(Unit unit)
    {
    }

    public override void UpdateState(Unit unit)
    {
    }
}
