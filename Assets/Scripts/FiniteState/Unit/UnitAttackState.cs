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
    }

    public override void FixedUpdateState(Unit unit)
    {
    }

    public override void UpdateState(Unit unit)
    {
    }
    public void DealDamage(Unit unit)
    {
        // Collider[] colliders =  Physics.OverlapBox(transform.TransformPoint(attackRange.transform.position), attackRange.size/2, Quaternion.identity,LayerMask.GetMask("Unit"));
        Vector3 boxCenter = unit.attackRange.transform.TransformPoint(unit.attackRange.center);
        float radius = unit.attackRange.radius;
        // Vector3 boxSize = attackRange.size / 2;
        // Quaternion boxRotation = attackRange.transform.rotation;
        // Debug.Log(boxCenter + " " + boxSize + " " + boxRotation);


        // Perform the OverlapBox check
        Collider[] colliders = Physics.OverlapSphere(
            boxCenter,
            radius,
            LayerMask.GetMask("Unit")
        );
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != unit.gameObject && collider.TryGetComponent<Unit>(out Unit checkUnit))
            {
                if (checkUnit.faction != unit.faction)
                {
                    // Debug.Log("Hit " + collider.gameObject.name);
                    // Apply damage to the enemy unit
                    collider.gameObject.GetComponent<Unit>().OnReceiveDamege(unit.Damage);
                }
            }
        }
    }
}
