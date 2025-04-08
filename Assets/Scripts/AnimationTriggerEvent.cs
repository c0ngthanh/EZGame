using UnityEngine;

public class AnimationTriggerEvent : MonoBehaviour
{
    [SerializeField] private Unit unit;
    public void OnAttackComplete()
    {
        if(!unit.CheckIfCanAttack()){
            unit.SwitchState(unit.unitIdleState);
        }
    }
    public void OnAttackHit()
    {
        unit.unitAttackState.DealDamage(unit);
    }
}
