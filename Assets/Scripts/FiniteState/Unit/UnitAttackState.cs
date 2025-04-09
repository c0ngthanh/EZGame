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
        Vector3 boxCenter = unit.transform.TransformPoint(unit.attackRange.center);
        Vector3 boxSize = unit.attackRange.size / 2;
        Quaternion boxRotation = Quaternion.Euler(0, unit.transform.eulerAngles.y, 0);

        // Perform the OverlapBox check
        Collider[] colliders = Physics.OverlapBox(
            boxCenter,
            boxSize,
            boxRotation,
            LayerMask.GetMask("Unit")
        );
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != unit.gameObject)
            {
                collider.gameObject.GetComponent<Unit>().OnReceiveDamege(unit.Damage);
            }
        }
    }
}
