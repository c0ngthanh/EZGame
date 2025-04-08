using UnityEngine;

public class UnitAttackState : UnitBaseState
{
    private int attackHash = Animator.StringToHash("Attack");
    public override void EnterState(Unit unit)
    {
        unit.PlayAnimation(Animator.StringToHash("Attack")); 
    }

    public override void ExitState(Unit unit)
    {
    }

    public override void FixedUpdateState(Unit unit)
    {
    }

    public override void UpdateState(Unit unit)
    {
    }
    public void DealDamage(Unit unit)
    {
        Collider[] colliders =  Physics.OverlapBox(unit.attackRange.transform.position, unit.attackRange.size/2, Quaternion.identity,LayerMask.GetMask("Unit"));
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != unit.gameObject)
            {
                collider.gameObject.GetComponent<Unit>().OnReceiveDamege(unit.Damage);
            }
        }
    }
}
