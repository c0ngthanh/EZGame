using UnityEngine;

public class UnitMoveState : UnitBaseState
{
    private int moveHash = Animator.StringToHash("Move");
    public override void EnterState(Unit unit)
    {
        unit.PlayAnimation(moveHash);
    }

    public override void ExitState(Unit unit)
    {
        Debug.Log("Exit Move State");
    }

    public override void UpdateState(Unit unit)
    {
    }
}
